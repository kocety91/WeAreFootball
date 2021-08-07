namespace WeAreFootball.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Common;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Events;
    using WeAreFootball.Web.ViewModels.Newses;
    using WeAreFootball.Web.ViewModels.Teams;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;
        private readonly IEventsService eventsService;
        private readonly ILeaguesService leagueService;
        private readonly ITagsService tagsService;
        private readonly ITeamsService teamsService;

        public NewsController(
            INewsService newsService,
            IEventsService eventsService,
            ILeaguesService leagueService,
            ITagsService tagsService,
            ITeamsService teamsService)
        {
            this.newsService = newsService;
            this.eventsService = eventsService;
            this.leagueService = leagueService;
            this.tagsService = tagsService;
            this.teamsService = teamsService;
        }

        public IActionResult ById(int id)
        {
            var currentNews = this.newsService.GetById<NewsViewModel>(id);
            currentNews.NewsTags = this.tagsService.GetTagsForNews<NewsTagViewModel>(currentNews.Id);
            currentNews.NewsLeagues = this.leagueService.GetleaguesForNews<NewsLeagueViewModel>(currentNews.Id);
            var leagueId = currentNews.NewsLeagues.Select(x => x.LeagueId).FirstOrDefault();
            currentNews.SimilarNews = this.newsService.GetLastFiveSimilarNews<SimilarNewsViewModel>(leagueId, currentNews.Id);

            if (currentNews == null)
            {
                return this.NotFound();
            }

            return this.View(currentNews);
        }

        public IActionResult Search(string searchString, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;

            var viewModel = new SearchedNewsViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetSearchedCount(searchString),
                News = this.newsService.Search<NewsViewModel>(searchString, id, itemsPerPage),
                Evets = this.eventsService.GetByTeamName<EventViewModel>(searchString),
            };

            return this.View(viewModel);
        }

        public IActionResult All(int leagueId, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;
            var countryName = this.leagueService.GetLeagueName(leagueId);

            var viewModel = new AllNewsViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetSearchedCount(countryName),
                News = this.newsService.GetNewsByCountry<NewsViewModel>(leagueId),
                Teams = this.teamsService.GetByLeagueId<TeamViewModel>(leagueId),
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.newsService.GetById<EditNewsInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditNewsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.newsService.UpdateAsync(id, input);
            return this.RedirectToAction("ById", new { id });
        }
    }
}
