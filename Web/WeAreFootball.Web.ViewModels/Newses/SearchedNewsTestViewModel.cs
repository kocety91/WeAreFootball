namespace WeAreFootball.Web.ViewModels.Newses
{
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class SearchedNewsTestViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string TagName { get; set; }
    }
}
