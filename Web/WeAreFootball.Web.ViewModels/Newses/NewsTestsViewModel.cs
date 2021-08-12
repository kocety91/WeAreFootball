namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Collections.Generic;

    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class NewsTestsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AddedByUserId { get; set; }

        public string Content { get; set; }

        public ICollection<NewsTagViewModel> Tags { get; set; }
    }
}
