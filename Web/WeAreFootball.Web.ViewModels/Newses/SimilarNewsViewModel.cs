namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class SimilarNewsViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortTitle
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Title, @"<[^>]+>", string.Empty));
                return content.Length > 30
                        ? content.Substring(0, 30) + "..."
                        : content;
            }
        }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 150
                        ? content.Substring(0, 150) + "..."
                        : content;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, SimilarNewsViewModel>()
            .ForMember(x => x.ImgUrl, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
            x.Image.RemoteImageUrl : "/images/news/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
