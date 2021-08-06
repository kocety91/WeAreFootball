namespace WeAreFootball.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        public string AddedByUser { get; set; }

        public string LeagueName { get; set; }

        public DateTime Date { get; set; }

        public string Sign { get; set; }

        public string Content { get; set; }

        public IEnumerable<EventTeamsViewModel> EventTeams { get; set; }

        public double AverageVote { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.LeagueName, y => y.MapFrom(x => x.League.Name))
                .ForMember(x => x.AddedByUser, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.ImgUrl, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
                x.Image.RemoteImageUrl : "/images/events/" + x.Image.Id + "." + x.Image.Extension))
                .ForMember(x => x.Date, y => y.MapFrom(x => x.CreatedOn))
                .ForMember(x => x.AverageVote, y => y.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)));
        }
    }
}
