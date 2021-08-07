namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Teams;

    public class TeamsController : AdministrationController
    {
        private readonly ITeamsService teamsService;
        private readonly IWebHostEnvironment environment;
        private readonly ILeaguesService leaguesService;

        public TeamsController(
            ITeamsService teamsService,
            IWebHostEnvironment environment,
            ILeaguesService leaguesService)
        {
            this.teamsService = teamsService;
            this.environment = environment;
            this.leaguesService = leaguesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateTeamViewModel();
            viewModel.LeagueItems = this.leaguesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.LeagueItems = this.leaguesService.GetAllAsKeyValuePairs();
                return this.RedirectToAction("Create");
            }

            var teamId = await this.teamsService.CreateAsync(input, $"{this.environment.WebRootPath}/images", $"{this.environment.WebRootPath}/images");
            return this.RedirectToAction("ById", "Teams", new { area = " ", id = teamId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.teamsService.DeleteAsync(id);
            return this.RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;
            var viewModel = new AllTeamsInTableViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.teamsService.GetCount(),
                Teams = this.teamsService.GetAll<TeamViewModel>(id, itemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
