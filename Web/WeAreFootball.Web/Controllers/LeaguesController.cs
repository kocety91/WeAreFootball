namespace WeAreFootball.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Leagues;

    public class LeaguesController : BaseController
    {
        private readonly ILeaguesService leaguesService;

        public LeaguesController(
            ILeaguesService leaguesService)
        {
            this.leaguesService = leaguesService;
        }

        public IActionResult All()
        {
            var viewModel = new AllLeaguesViewModel()
            {
                Leagues = this.leaguesService.All<LeagueViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var league = this.leaguesService.GetById<LeagueViewModel>(id);
            return this.View(league);
        }
    }
}
