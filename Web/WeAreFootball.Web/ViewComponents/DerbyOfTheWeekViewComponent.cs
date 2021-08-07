namespace WeAreFootball.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;

    public class DerbyOfTheWeekViewComponent : ViewComponent
    {
        private readonly IEventsService eventsService;

        public DerbyOfTheWeekViewComponent(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IViewComponentResult Invoke()
        {
            var derby = this.eventsService.GetLatestDerbyOfTheWeek<DerbyOfTheWeekViewModel>();

            return this.View(derby);
        }
    }
}
