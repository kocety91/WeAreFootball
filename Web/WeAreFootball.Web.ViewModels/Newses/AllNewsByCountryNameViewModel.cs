namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Collections.Generic;

    using WeAreFootball.Web.ViewModels.Teams;

    public class AllNewsByCountryNameViewModel : NewsCountryPagingViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}
