namespace WeAreFootball.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Newses;
    using WeAreFootball.Web.ViewModels.Teams;

    public class TeamsController : BaseController
    {
        private readonly ITeamsService teamsService;
        private readonly IEventsService eventsService;
        private readonly INewsService newsService;

        public TeamsController(
            ITeamsService teamsService,
            IEventsService eventsService,
            INewsService newsService)
        {
            this.teamsService = teamsService;
            this.eventsService = eventsService;
            this.newsService = newsService;
        }

        public IActionResult ById(int id)
        {
            var team = this.teamsService.GetById<TeamViewModel>(id);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                team.Events = this.eventsService.GetByTeamId<EventViewModel>(team.Id);
                team.News = this.newsService.GetNewsByTeamName<NewsViewModel>(team.Name);
                return this.View(team);
            }
        }

        public IActionResult ByTagName(string tagName)
        {
            var team = this.teamsService.GetByTagName<TeamViewModel>(tagName);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                team.Events = this.eventsService.GetByTeamId<EventViewModel>(team.Id);
                team.News = this.newsService.GetNewsByTeamName<NewsViewModel>(team.Name);
                return this.View(team);
            }
        }
    }
}
