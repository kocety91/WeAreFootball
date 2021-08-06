namespace WeAreFootball.Web.ViewModels.Leagues
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels.Teams;

    public class LeagueViewModel : IMapFrom<League>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string ImgUrl { get; set; }

        public string LogoUrl { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; }

        public int TeamsCount => this.Teams.Count();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<League, LeagueViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.ImgUrl, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
                x.Image.RemoteImageUrl : "/images/leagues/" + x.Image.Id + "." + x.Image.Extension))
                .ForMember(x => x.LogoUrl, y => y.MapFrom(x => x.Logo.RemoteImageUrl != null ?
                x.Logo.RemoteImageUrl : "/images/leagues/" + x.Logo.Id + "." + x.Logo.Extension));
        }
    }
}
