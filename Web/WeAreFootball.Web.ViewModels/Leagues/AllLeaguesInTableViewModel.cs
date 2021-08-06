namespace WeAreFootball.Web.ViewModels.Leagues
{
    using System.Collections.Generic;

    public class AllLeaguesInTableViewModel : DashboardPagingViewModel
    {
        public IEnumerable<LeagueViewModel> Leagues { get; set; }
    }
}
