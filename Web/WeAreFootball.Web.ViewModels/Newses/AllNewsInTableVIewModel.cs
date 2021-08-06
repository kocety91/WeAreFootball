namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Collections.Generic;

    public class AllNewsInTableVIewModel : DashboardPagingViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
