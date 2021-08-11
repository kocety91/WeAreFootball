namespace WeAreFootball.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using WeAreFootball.Common;
    using WeAreFootball.Data;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Data.Repositories;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels;
    using WeAreFootball.Web.ViewModels.Newses;
    using Xunit;

    public class NewsServiceTests
    {
        public NewsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task DeleteShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var news = new News()
            {
                Title = "RandomTitle",
                Content = "RandomContent",
                AddedByUserId = "1",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            await service.DeleteAsync(1);

            var actual = await dbContext.News.CountAsync();

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteShouldThrowNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var news = new News()
            {
                Id = 1,
                Title = "RandomTitle",
                Content = "RandomContent",
                AddedByUserId = "1",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteAsync(2));
            Assert.Equal(string.Format(ExceptionMessages.NewsDoesntExists), exception.Message);
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<News>()
            {
               new News { Id = 1, Title = "RandomTitle1", Content = "RandomContent1",   AddedByUserId = "1" },
               new News { Id = 2, Title = "RandomTitle2", Content = "RandomContent2",   AddedByUserId = "2" },
               new News { Id = 3, Title = "RandomTitle3", Content = "RandomContent3",   AddedByUserId = "3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.GetAll<NewsTestsViewModel>(1, 6);
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetByIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var news = new News()
            {
                Id = 1,
                Title = "RandomTitle1",
                Content = "RandomContent1",
                AddedByUserId = "1",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var expected = new NewsTestsViewModel()
            {
                Id = 1,
                Title = "RandomTitle1",
                Content = "RandomContent1",
                AddedByUserId = "1",
            };

            var actual = service.GetById<NewsTestsViewModel>(1);
            Assert.Equal(JsonConvert.SerializeObject(expected.Title), JsonConvert.SerializeObject(actual.Title));
        }

        [Fact]
        public async Task GetCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var news = new News { Id = 1, Title = "RandomTitle1", Content = "RandomContent1", AddedByUserId = "1" };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.GetCount();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetTodayNewsCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var today = DateTime.UtcNow;
            DateTime dateValue;
            var yesterday = DateTime.TryParse("1999-08-08", out dateValue);

            var news = new List<News>()
            {
                new News() { Id = 1, Title = "RandomTitle1", CreatedOn = today, AddedByUserId = "1" },
                new News() { Id = 2, Title = "RandomTitle2", CreatedOn = dateValue, AddedByUserId = "1" },
                new News() { Id = 3, Title = "RandomTitle3", CreatedOn = dateValue, AddedByUserId = "1" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddRangeAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.GetTodayNewsCount(today);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetNewsByCountryCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League()
            {
                Id = 1,
                Name = "Serie A",
                Country = "Italy",
            };

            var newsLeague = new NewsLeague()
            {
                NewsId = 1,
                League = league,
            };

            var news = new News()
            {
                Id = 1,
                Title = "RandomTitle1",
                AddedByUserId = "1",
            };

            news.Leagues.Add(newsLeague);

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddRangeAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.GetNewsByCountryCount(league.Id);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task SearchShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var tag = new Tag()
            {
                Id = 1,
                Name = "Roma",
            };

            var news = new News()
            {
                Id = 1,
                Title = "Roma",
                AddedByUserId = "1",
            };

            var newsTag = new NewsTag()
            {
                News = news,
                Tag = tag,
            };

            news.Tags.Add(newsTag);

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddRangeAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.Search<SearchedNewsTestViewModel>("Roma", 1, 6);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task GetNewsByCountryShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League()
            {
                Id = 1,
                Name = "Serie A",
                Country = "Italy",
            };

            var newsLeague = new NewsLeague()
            {
                NewsId = 1,
                League = league,
            };

            var news = new News()
            {
                Id = 1,
                Title = "RandomTitle1",
                AddedByUserId = "1",
            };

            news.Leagues.Add(newsLeague);

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.News.AddRangeAsync(news);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var result = service.GetNewsByCountry<NewsTestsViewModel>(league.Id, 1, 6);

            Assert.Equal(1, result.Count());
        }

        private void InitializeMapper() => AutoMapperConfig.
      RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
