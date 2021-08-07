namespace WeAreFootball.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;

    public class EventsController : BaseController
    {
        private readonly IEventsService eventsService;

        public EventsController(
            IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;

            var viewModel = new AllEventsViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.eventsService.GetCount(),
                Events = this.eventsService.GetAll<EventViewModel>(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var currentEvent = this.eventsService.GetById<EventViewModel>(id);
            return this.View(currentEvent);
        }

        public IActionResult ByLeagueId(int leagueId)
        {
            var itemsPerPage = 6;
            var pageId = 1;

            var viewModel = new AllEventsViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = pageId,
                Count = this.eventsService.GetCount(),
                Events = this.eventsService.GetByLeagueId<EventViewModel>(leagueId, pageId, itemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
