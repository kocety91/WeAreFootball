namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Collections.Generic;

    using WeAreFootball.Web.ViewModels.Events;

    public class SearchedNewsViewModel : PagingViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }

        public IEnumerable<EventViewModel> Evets { get; set; }
    }
}
