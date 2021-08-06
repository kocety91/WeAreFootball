namespace WeAreFootball.Web.ViewModels.Newses
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using Ganss.XSS;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Mapping;

    public class NewsViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ImgUrl { get; set; }

        public string Source { get; set; }

        public string AddedByUser { get; set; }

        public string UserImage { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<NewsCommentViewModel> Comments { get; set; }

        public IEnumerable<SimilarNewsViewModel> SimilarNews { get; set; }

        public IEnumerable<NewsTagViewModel> NewsTags { get; set; }

        public IEnumerable<NewsLeagueViewModel> NewsLeagues { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, NewsViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.AddedByUser, y => y.MapFrom(x => x.AddedByUser.UserName))
                .ForMember(x => x.ImgUrl, y => y.MapFrom(x => x.Image.RemoteImageUrl != null ?
                x.Image.RemoteImageUrl : "/images/news/" + x.Image.Id + "." + x.Image.Extension))
                .ForMember(x => x.UserImage, y => y.MapFrom(u => u.AddedByUser.Image.RemoteImageUrl != null ?
                   u.AddedByUser.Image.RemoteImageUrl : "/images/users/" + u.AddedByUser.ImageId + "." +
                   u.AddedByUser.Image.Extension));
        }
    }
}
