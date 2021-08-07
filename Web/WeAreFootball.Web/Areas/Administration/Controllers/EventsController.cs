namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;

    public class EventsController : AdministrationController
    {
        private readonly IEventsService eventsService;
        private readonly IWebHostEnvironment environment;
        private readonly ITeamsService teamsService;
        private readonly ILeaguesService leaguesService;

        public EventsController(
            IEventsService eventsService,
            IWebHostEnvironment environment,
            ITeamsService teamsService,
            ILeaguesService leaguesService)
        {
            this.eventsService = eventsService;
            this.environment = environment;
            this.teamsService = teamsService;
            this.leaguesService = leaguesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateEventViewModel();
            viewModel.LeagueItems = this.leaguesService.GetAllAsKeyValuePairs();
            viewModel.TeamsItems = this.teamsService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.TeamsItems = this.teamsService.GetAllAsKeyValuePairs();
                input.LeagueItems = this.leaguesService.GetAllAsKeyValuePairs();
                return this.RedirectToAction("Create");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var eventId = await this.eventsService.CreateAsync(input, userId, $"{this.environment.WebRootPath}/images");
            return this.RedirectToAction("ById", "Events", new { area = " ", id = eventId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.eventsService.DeleteAsync(id);
            return this.RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;

            var viewModel = new AllEventsInTableViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.eventsService.GetCount(),
                Events = this.eventsService.GetAll<EventViewModel>(id, itemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
