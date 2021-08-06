namespace WeAreFootball.Services
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp;
    using AngleSharp.Dom;
    using WeAreFootball.Data.Models;

    public class Scraper
    {
        public async Task<List<News>> Scrape(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var document = await context.OpenAsync("https://sports.ndtv.com/football/news");

            var ul = document.QuerySelector("#container_listing");
            var li = ul.QuerySelectorAll("li.lst-pg-a-li > div.lst-pg-a > div.lst-pg_txt-wrp > a");

            var newsCollection = new List<News>();

            foreach (var item in li)
            {
                var link = $"https://sports.ndtv.com{item.GetAttribute("href")}";

                var getInfo = new BrowsingContext(config).OpenAsync(link).GetAwaiter().GetResult();

                var news = this.GetDesciption(getInfo);

                if (news != null && news.Content.Length != 0)
                {
                    newsCollection.Add(news);
                }
            }

            return newsCollection;
        }

        private News GetDesciption(IDocument originalPage)
        {
            if (originalPage == null)
            {
                return null;
            }

            var article = originalPage.QuerySelector("article.vjl-lg-9");
            var title = this.GetTitle(article);
            var content = this.GetContent(article);
            var image = this.GetImageUrl(article);
            var tags = this.GetTags(article);

            if (title == null || content == null || image == null)
            {
                return null;
            }

            var currentNews = new News()
            {
                Title = title,
                Content = content,
                Image = new Image()
                {
                    RemoteImageUrl = image,
                },
            };

            foreach (var tag in tags)
            {
                var currentTag = new Tag()
                {
                    Name = tag.TextContent.Trim(),
                };

                if (currentTag != null)
                {
                    currentNews.Tags.Add(new NewsTag
                    {
                        Tag = currentTag,
                        News = currentNews,
                    });
                }
            }

            return currentNews;
        }

        private string GetTitle(IElement article)
        {
            if (article == null)
            {
                return null;
            }

            var title = article.QuerySelector("h1.sp-ttl").TextContent;

            return title;
        }

        private string GetContent(IElement article)
        {
            var sb = new StringBuilder();

            try
            {
                var elements = article.QuerySelectorAll("div.story__content > div > p");

                foreach (var element in elements)
                {
                    sb.AppendLine(element.TextContent);
                }

                return sb.ToString().TrimEnd();
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        private string GetImageUrl(IElement article)
        {
            if (article == null)
            {
                return null;
            }

            var image = article.QuerySelector("div.ins_instory_dv_cont > img").GetAttribute("src");

            return image;
        }

        private IHtmlCollection<IElement> GetTags(IElement article)
        {
            if (article == null)
            {
                return null;
            }

            var tags = article.QuerySelectorAll("div.tg_wrp > a");

            return tags;

        }

    }
}
