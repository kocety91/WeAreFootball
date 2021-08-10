namespace WeAreFootball.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels.Newses;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<League> leagueReposiory;
        private readonly IDeletableEntityRepository<Tag> tagRepository;
        private readonly IRepository<NewsTag> newsTagsRepository;
        private readonly IRepository<NewsLeague> newsLeaguesRepository;

        public NewsService(
            IDeletableEntityRepository<News> newsRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<League> leagueReposiory,
            IDeletableEntityRepository<Tag> tagRepository,
            IRepository<NewsTag> newsTagsRepository,
            IRepository<NewsLeague> newsLeaguesRepository)
        {
            this.newsRepository = newsRepository;
            this.teamsRepository = teamsRepository;
            this.leagueReposiory = leagueReposiory;
            this.tagRepository = tagRepository;
            this.newsTagsRepository = newsTagsRepository;
            this.newsLeaguesRepository = newsLeaguesRepository;
        }

        public async Task<int> CreateAsync(CreateNewsViewModel input, string userId, string imagePath)
        {
            var currnetNews = this.newsRepository.All().Where(x => x.Title == input.Title).FirstOrDefault();

            if (currnetNews == null)
            {
                currnetNews = new News()
                {
                    Title = input.Title,
                    Content = input.Content,
                    AddedByUserId = userId,
                };
            }
            else
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.NewsAlreadyExists, input.Title));
            }

            foreach (var id in input.LeagueIds)
            {
                var currentLeague = this.leagueReposiory.All().Where(x => x.Id == id).FirstOrDefault();

                if (currentLeague != null)
                {
                    currnetNews.Leagues.Add(new NewsLeague
                    {
                        League = currentLeague,
                        News = currnetNews,
                    });
                }
            }

            foreach (var tagId in input.TagsIds)
            {
                var currentTag = this.tagRepository.All().Where(x => x.Id == tagId).FirstOrDefault();

                if (currentTag != null)
                {
                    currnetNews.Tags.Add(new NewsTag
                    {
                        Tag = currentTag,
                        News = currnetNews,
                    });
                }
            }

            Directory.CreateDirectory($"{imagePath}/news/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            await this.newsRepository.AddAsync(currnetNews);
            await this.newsRepository.SaveChangesAsync();

            currnetNews.Image = new Image()
            {
                NewsId = currnetNews.Id,
                News = currnetNews,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/news/{currnetNews.Image.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.newsRepository.SaveChangesAsync();

            return currnetNews.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var news = this.newsRepository.All().FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.NewsDoesntExists));
            }

            this.newsRepository.Delete(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            return this.newsRepository.All().OrderByDescending(x => x.CreatedOn)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.newsRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
        }

        public async Task UpdateAsync(int id, EditNewsInputModel input)
        {
            var updatedNews = this.newsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (updatedNews != null)
            {
                updatedNews.Title = input.Title;
                updatedNews.Content = input.Content;
            }

            await this.newsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.newsRepository.All().Count();
        }

        public int GetSearchedCount(string name)
        {
            return this.newsRepository.All()
                .Where(x => x.Title.Contains(name)
                || x.Content.Contains(name))
                .Count();
        }

        public IEnumerable<T> GetNewsByTeamName<T>(string teamName)
        {
            return this.newsRepository.All()
                .Where(x => x.Tags.Select(n => n.Tag.Name).Contains(teamName))
                .OrderByDescending(c => c.CreatedOn)
                .Take(6)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetMostComment<T>()
        {
            var currentDate = DateTime.Now.Day;
            return this.newsRepository.All()
                .Where(x => x.CreatedOn.Day == currentDate)
                .OrderByDescending(x => x.Comments.Count).Take(5).To<T>().ToList();
        }

        public int GetTodayNewsCount(DateTime today)
        {
            return this.newsRepository.All()
                .Where(x => x.CreatedOn.Day == today.Day)
                .Count();
        }

        public int GetNewsByCountryCount(int countryId)
        {
           return this.newsRepository.All()
                .Where(x => x.Leagues.Any(l => l.League.Id == countryId))
                .Count();
        }

        public IEnumerable<T> GetLastFiveSimilarNews<T>(int leagueId, int newsId)
        {
            return this.newsLeaguesRepository.All()
                .Where(x => x.LeagueId == leagueId && x.NewsId != newsId)
                .Select(n => n.News)
                .Take(5)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>().ToList();
        }

        public IEnumerable<T> Search<T>(string searchString, int page, int itemsPerPage = 6)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return this.newsTagsRepository.All()
                    .Where(x => x.Tag.Name.ToLower() == searchString.ToLower())
                    .Select(t => t.News)
                    .OrderByDescending(x => x.CreatedOn)
                    .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                    .To<T>().ToList();
            }

            return null;
        }

        public IEnumerable<T> GetNewsByCountry<T>(int leagueId, int page, int itemsPerPage = 6)
        {
            return this.newsLeaguesRepository.All()
                .Where(x => x.LeagueId == leagueId)
                .Select(n => n.News)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public async Task Scrape()
        {
            var scraper = new Scraper();
            var news = await scraper.Scrape("https://sports.ndtv.com/football/news");

            foreach (var item in news)
            {
                var currentNews = await this.SetNewsLeagues(item);

                if (currentNews == null)
                {
                    continue;
                }

                foreach (var newsTag in item.Tags)
                {
                    var currentTag = await this.GetTag(newsTag);
                    await this.AddNewsTags(currentTag, currentNews);
                }
            }
        }

        private List<League> GetAllLeagues(News news)
        {
            var tags = news.Tags.Select(x => x.Tag.Name).ToList();

            var searchedLeagues = this.leagueReposiory.All().Where(x => tags.Contains(x.Name) || tags.Contains(x.Country)).ToList();

            if (searchedLeagues.Count == 0)
            {
                var ss = this.leagueReposiory.All().Where(l => l.Teams.Any(t => tags.Contains(t.Name))).ToList();

                foreach (var league in ss)
                {
                    var currentLeague = league;
                    searchedLeagues.Add(currentLeague);
                }
            }

            return searchedLeagues;
        }

        private async Task AddNewsTags(Tag tagInput, News newsInput)
        {
            var newsTag = this.newsTagsRepository.All()
                .Where(x => x.News.Title == newsInput.Title && x.Tag.Name == tagInput.Name)
                .FirstOrDefault();

            if (newsTag == null)
            {
                newsTag = new NewsTag()
                {
                    Tag = tagInput,
                    News = newsInput,
                };
                await this.newsTagsRepository.AddAsync(newsTag);
                await this.newsTagsRepository.SaveChangesAsync();
            }
        }

        private async Task<News> SetNewsLeagues(News newsNow)
        {
            var leagues = this.GetAllLeagues(newsNow);

            if (!leagues.Any())
            {
                return null;
            }

            var currentNews = this.newsRepository.All().Where(x => x.Title == newsNow.Title).FirstOrDefault();

            if (currentNews == null)
            {
                currentNews = new News()
                {
                    Title = newsNow.Title,
                    Content = newsNow.Content,
                    Image = newsNow.Image,
                    Source = "sports.ndtv.com",
                };

                foreach (var league in leagues)
                {
                    var newsLeague = new NewsLeague()
                    {
                        League = league,
                        News = currentNews,
                    };

                    currentNews.Leagues.Add(newsLeague);
                }

                await this.newsRepository.AddAsync(currentNews);
                await this.newsRepository.SaveChangesAsync();
                return currentNews;
            }
            else
            {
                currentNews = null;
                return currentNews;
            }
        }

        private async Task<Tag> GetTag(NewsTag tagNews)
        {
            var currentTag = this.tagRepository.All().Where(x => x.Name == tagNews.Tag.Name).FirstOrDefault();

            if (currentTag == null)
            {
                currentTag = new Tag() { Name = tagNews.Tag.Name };
                await this.tagRepository.AddAsync(currentTag);
                await this.tagRepository.SaveChangesAsync();
            }

            return currentTag;
        }
    }
}
