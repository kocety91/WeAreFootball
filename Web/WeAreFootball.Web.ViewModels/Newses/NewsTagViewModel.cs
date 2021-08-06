namespace WeAreFootball.Web.ViewModels.Newses
{
    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class NewsTagViewModel : IMapFrom<NewsTag>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewsTag, NewsTagViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.TagName, y => y.MapFrom(x => x.Tag.Name));
        }
    }
}
