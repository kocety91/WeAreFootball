namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Dashboard;
    using WeAreFootball.Web.ViewModels.Newses;
    using WeAreFootball.Web.ViewModels.Users;

    public class DashboardController : AdministrationController
    {
        private readonly INewsService newsService;
        private readonly IUsersService usersService;

        public DashboardController(
            INewsService newsService,
            IUsersService usersService
            )
        {
            this.newsService = newsService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var todayDate = DateTime.Now;
            var viewModel = new InputModel()
            {
                NewsToday = this.newsService.GetTodayNewsCount(todayDate),
                UsersToday = this.usersService.GetRegisteredUsersToday(todayDate),
                MostActiveUsers = this.usersService.GetMostActiveUsers<UserViewModel>(),
                MostCommentNews = this.newsService.GetMostComment<NewsViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
