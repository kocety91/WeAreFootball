namespace WeAreFootball.Web.ViewModels.Newses
{
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class EditNewsInputModel : BaseNewsViewModel, IMapFrom<News>
    {
        public int Id { get; set; }
    }
}
