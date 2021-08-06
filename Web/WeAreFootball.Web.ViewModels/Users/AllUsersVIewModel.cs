namespace WeAreFootball.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class AllUsersVIewModel : DashboardPagingViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
