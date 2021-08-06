namespace WeAreFootball.Web.ViewModels.Teams
{
    using System.Collections.Generic;

    public class AllTeamsViewModel : PagingViewModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}
