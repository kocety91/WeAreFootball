namespace WeAreFootball.Web.ViewModels.Teams
{
    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class TeamTestViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string StadiumName { get; set; }

        public int LeagueId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Team, TeamViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id));

        }
    }
}
