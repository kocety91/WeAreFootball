namespace WeAreFootball.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Leagues;
    using WeAreFootball.Web.ViewModels.NavBars;

    public class NavBarComponent : ViewComponent
    {
        private readonly ILeaguesService leaguesService;
        private readonly IEventsService eventsService;

        public NavBarComponent(
            ILeaguesService leaguesService,
            IEventsService eventsService)
        {
            this.leaguesService = leaguesService;
            this.eventsService = eventsService;
        }

        public IViewComponentResult Invoke()
        {
            var myNavBar = new NavBarViewModel()
            {
                Leagues = this.leaguesService.GetAll<LeagueViewModel>(),
                LastFourEvents = this.eventsService.GetLastFour<EventViewModel>(),
            };

            return this.View(myNavBar);
        }
    }
}
