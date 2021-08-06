namespace WeAreFootball.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class DerbyOfTheWeekViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AddedByUser { get; set; }

        public string LeagueName { get; set; }

        public string LeagueLogo { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<EventTeamsViewModel> EventTeams { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, DerbyOfTheWeekViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Date, y => y.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.LeagueLogo, y => y.MapFrom(x => x.League.Logo.RemoteImageUrl != null ?
                  x.League.Logo.RemoteImageUrl : "/images/leagues/" + x.League.Logo.Id + "." + x.League.Logo.Extension))
                .ForMember(x => x.LeagueName, y => y.MapFrom(x => x.League.Name))
                .ForMember(x => x.AddedByUser, y => y.MapFrom(x => x.User.UserName));
        }
    }
}
