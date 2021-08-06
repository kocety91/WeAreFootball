namespace WeAreFootball.Web.ViewModels.Dashboard
{
    using System.Collections.Generic;

    public class InputModel
    {
        public int UsersToday { get; set; }

        public int NewsToday { get; set; }

        public IEnumerable<UserViewModel> MostActiveUsers { get; set; }

        public IEnumerable<NewsViewModel> MostCommentNews { get; set; }
    }
}
