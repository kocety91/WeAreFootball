namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Newses;

    public class NewsController : AdministrationController
    {
        private readonly INewsService newsService;
        private readonly ITagsService tagsService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILeaguesService leagueService;

        public NewsController(
            INewsService newsService,
            ITagsService tagsService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager,
            ILeaguesService leagueService)
        {
            this.newsService = newsService;
            this.tagsService = tagsService;
            this.environment = environment;
            this.userManager = userManager;
            this.leagueService = leagueService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateNewsViewModel();
            viewModel.Leagues = this.leagueService.GetAllAsKeyValuePairs();
            viewModel.Tags = this.tagsService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Leagues = this.leagueService.GetAllAsKeyValuePairs();
                input.Tags = this.tagsService.GetAllAsKeyValuePairs();
                return this.RedirectToAction("Create");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var newsId = await this.newsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            return this.RedirectToAction("ById", "News", new { area = " ", id = newsId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteAsync(id);
            return this.RedirectToAction("GetAll");
        }

        public IActionResult GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;

            var viewModel = new AllNewsInTableVIewModel()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetCount(),
                News = this.newsService.GetAll<NewsViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
