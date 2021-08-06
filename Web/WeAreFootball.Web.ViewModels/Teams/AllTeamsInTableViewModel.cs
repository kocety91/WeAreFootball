namespace WeAreFootball.Web.ViewModels.Teams
{
    using System.Collections.Generic;

    public class AllTeamsInTableViewModel : DashboardPagingViewModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}
