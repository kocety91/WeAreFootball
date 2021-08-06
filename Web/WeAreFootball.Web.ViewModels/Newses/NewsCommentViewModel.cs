namespace WeAreFootball.Web.ViewModels.Newses
{
    using System;

    using AutoMapper;
    using Ganss.XSS;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class NewsCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ApplicationUserUserName { get; set; }

        public string ApplicationUserImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, NewsCommentViewModel>()
                .ForMember(x => x.ApplicationUserImage, y => y.MapFrom(x => x.ApplicationUser.Image.RemoteImageUrl != null ?
                x.ApplicationUser.Image.RemoteImageUrl : "/images/users/" + x.ApplicationUser.ImageId + "." + x.ApplicationUser.Image.Extension));
        }
    }
}
