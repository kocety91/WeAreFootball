namespace WeAreFootball.Web.ViewModels.Events
{
    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class EventTeamsViewModel : IMapFrom<EventTeams>, IHaveCustomMappings
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public int EventId { get; set; }

        public string LogoUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EventTeams, EventTeamsViewModel>()
                .ForMember(x => x.TeamName, y => y.MapFrom(x => x.Team.Name))
                .ForMember(x => x.LogoUrl, y => y.MapFrom(x => x.Team.Logo.RemoteImageUrl != null ?
                x.Team.Logo.RemoteImageUrl : "/images/teams/" + x.Team.Logo.Id + "." + x.Team.Logo.Extension));
        }
    }
}
