namespace WeAreFootball.Web.ViewModels.NavBars
{
    using System.Collections.Generic;

    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Leagues;

    public class NavBarViewModel
    {
        public IEnumerable<LeagueViewModel> Leagues { get; set; }

        public IEnumerable<EventViewModel> LastFourEvents { get; set; }
    }
}
