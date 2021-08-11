namespace WeAreFootball.Web.ViewModels.Events
{
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class EventTestViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public int LeagueId { get; set; }

        public string Content { get; set; }

        public bool IsDerbyOfTheWeek { get; set; }

        public int TeamId { get; set; }
    }
}
