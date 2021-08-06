namespace WeAreFootball.Web.ViewModels.Newses
{
    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class NewsLeagueViewModel : IMapFrom<NewsLeague>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public string Country { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewsLeague, NewsLeagueViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
               .ForMember(x => x.LeagueId, y => y.MapFrom(x => x.LeagueId))
               .ForMember(x => x.Country, y => y.MapFrom(x => x.League.Country))
               .ForMember(x => x.LeagueName, y => y.MapFrom(x => x.League.Name));
        }
    }
}
