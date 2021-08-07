namespace WeAreFootball.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Users;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult GetAll(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var itemsPerPage = 6;

            var viewModel = new AllUsersVIewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.usersService.GetCount(),
                Users = this.usersService.GetAll<UserViewModel>(id, itemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
