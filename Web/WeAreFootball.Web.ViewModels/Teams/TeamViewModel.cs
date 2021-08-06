namespace WeAreFootball.Web.ViewModels.Teams
{
    using System.Collections.Generic;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Newses;

    public class TeamViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string StadiumName { get; set; }

        public string Country { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public string ImageId { get; set; }

        public string ImgUrl { get; set; }

        public string LogoId { get; set; }

        public string LogoUrl { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; }

        public IEnumerable<NewsViewModel> News { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Team, TeamViewModel>()
                 .ForMember(x => x.LeagueId, y => y.MapFrom(x => x.LeagueId))
                 .ForMember(x => x.ImageId, y => y.MapFrom(x => x.ImageId))
                 .ForMember(x => x.LogoId, y => y.MapFrom(x => x.LogoId))
                 .ForMember(x => x.LeagueName, y => y.MapFrom(x => x.League.Name))
                 .ForMember(x => x.Country, y => y.MapFrom(x => x.League.Country))
                 .ForMember(x => x.ImgUrl, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
                 x.Image.RemoteImageUrl : "/images/teams/" + x.Image.Id + "." + x.Image.Extension))
                 .ForMember(x => x.LogoUrl, y => y.MapFrom(x => x.Logo.RemoteImageUrl != null ?
                 x.Logo.RemoteImageUrl : "/images/teams/" + x.Logo.Id + "." + x.Logo.Extension));
        }
    }
}
