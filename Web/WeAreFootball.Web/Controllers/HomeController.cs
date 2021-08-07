namespace WeAreFootball.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels;
    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Home;
    using WeAreFootball.Web.ViewModels.Leagues;
    using WeAreFootball.Web.ViewModels.Newses;

    public class HomeController : BaseController
    {
        private readonly INewsService newsService;
        private readonly IEventsService eventsService;
        private readonly ILeaguesService leaguesService;

        public HomeController(
            INewsService newsService,
            IEventsService eventsService,
            ILeaguesService leaguesService)
        {
            this.newsService = newsService;
            this.eventsService = eventsService;
            this.leaguesService = leaguesService;
        }

        public IActionResult Index(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;
            var viewModel = new IndexViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetCount(),
                News =
                    this.newsService.GetAll<NewsViewModel>(id, itemsPerPage),
                MostCommentNews = this.newsService.GetMostComment<NewsViewModel>(),
                EventsToday = this.eventsService.TodayEvents<EventViewModel>(),
                LeaguesCountry = this.leaguesService.GetAll<LeagueViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(HttpErrorViewModel errorViewModel)
        {
            if (errorViewModel.StatusCode == 404)
            {
                return this.View(
                "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
            }

            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(HttpErrorViewModel errorViewModel)
        {
            if (errorViewModel.StatusCode == 404)
            {
                return this.View(errorViewModel);
            }

            return this.View(
                "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
