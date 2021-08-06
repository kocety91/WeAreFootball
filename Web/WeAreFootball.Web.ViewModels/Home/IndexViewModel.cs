namespace WeAreFootball.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Leagues;
    using WeAreFootball.Web.ViewModels.Newses;

    public class IndexViewModel : IndexPagingViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }

        public IEnumerable<EventViewModel> EventsToday { get; set; }

        public IEnumerable<NewsViewModel> MostCommentNews { get; set; }

        public IEnumerable<LeagueViewModel> LeaguesCountry { get; set; }
    }
}
