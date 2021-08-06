namespace WeAreFootball.Web.ViewModels.Users
{
    using System;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public int CommentsCount { get; set; }

        public DateTime RegisterdOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserViewModel>()
              .ForMember(x => x.Image, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
                 x.Image.RemoteImageUrl : "/images/users/" + x.Image.Id + "." + x.Image.Extension))
              .ForMember(x => x.RegisterdOn, y => y.MapFrom(x => x.CreatedOn))
              .ForMember(x => x.CommentsCount, y => y.MapFrom(x => x.Comments.Count));
        }
    }
}
