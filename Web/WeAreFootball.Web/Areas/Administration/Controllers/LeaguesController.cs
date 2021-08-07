namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Leagues;

    public class LeaguesController : AdministrationController
    {
        private readonly ILeaguesService leaguesService;
        private readonly IWebHostEnvironment environment;

        public LeaguesController(
            ILeaguesService leaguesService,
            IWebHostEnvironment environment)
        {
            this.leaguesService = leaguesService;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateLeagueViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeagueViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create");
            }

            var leagueId = await this.leaguesService.CreateAsync(input, $"{this.environment.WebRootPath}/images", $"{this.environment.WebRootPath}/images");
            return this.RedirectToAction("ById", "Leagues", new { area = " ", id = leagueId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.leaguesService.DeleteAsync(id);
            return this.RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;

            var viewModel = new AllLeaguesInTableViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.leaguesService.GetCount(),
                Leagues = this.leaguesService.ShowAll<LeagueViewModel>(id, itemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
