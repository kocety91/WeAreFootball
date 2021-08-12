namespace WeAreFootball.Services.Data.Tests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
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
        private readonly string ImageExtension = "test.jpg";

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

        [Fact]
        public async Task GetNewsByTeamNameShouldWorksCorrectly()
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
                Title = "RandomTitle1",
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

            var result = service.GetNewsByTeamName<NewsTestsViewModel>("Roma");

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task EditAsyncShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var input = this.SeedInputModel(this.ImageExtension);
            await service.CreateAsync(input, "1", "path");

            var editViewModel = new EditNewsInputModel()
            {
                Id = 1,
                Title = "just testing",
                Content = "keep testing",
            };

            await service.UpdateAsync(1, editViewModel);

            var actual = await dbContext.News.FirstOrDefaultAsync();

            Assert.Equal(editViewModel.Title, actual.Title);
            Assert.Equal(editViewModel.Content, actual.Content);
            Assert.Equal(editViewModel.Id, actual.Id);
        }

        [Fact]
        public async Task CreateAsyncShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var input = this.SeedInputModel(this.ImageExtension);
            await service.CreateAsync(input, "1", "path");

            var expected = this.SeedDataBaseNews(1, "fake title", "1", "tagOne", dbContext);

            var actual = await dbContext.News.FirstOrDefaultAsync();
            var count = await dbContext.News.CountAsync();

            Assert.IsType<News>(actual);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowInvalidOperationExceptionWhenImageExtensionIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<News>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);
            using var repository3 = new EfDeletableEntityRepository<League>(dbContext);
            using var repository4 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository5 = new EfRepository<NewsTag>(dbContext);
            using var repository6 = new EfRepository<NewsLeague>(dbContext);

            var service = new NewsService(repository, repository2, repository3, repository4, repository5, repository6);

            var viewModel = this.SeedInputModel("test.pdf");

            await service.CreateAsync(viewModel, "1", "path");

            var exception = await Assert
                 .ThrowsAsync<NullReferenceException>(async () => await service.CreateAsync(viewModel, "1", "path"));
            Assert.Equal(string.Format(ExceptionMessages.NewsAlreadyExists, "fake title"), exception.Message);
        }

        private CreateNewsViewModel SeedInputModel(string extension)
        {
            var title = "fake title";
            var content = "fake content";
            var fileName = extension;
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return new CreateNewsViewModel
            {
                Title = title,
                Content = content,
                LeagueIds = new[] { 1, 2 },
                TagsIds = new[] { 1, 2 },
                Image = new FormFile(stream, 0, stream.Length, "createNews", fileName),
            };
        }

        private News SeedDataBaseNews(int id, string title, string imgId, string tagOne, ApplicationDbContext dbContext)
        {
            var tag1 = new Tag()
            {
                Id = 1,
                Name = tagOne,
            };

            var news = new News()
            {
                Id = id,
                Title = title,
                Content = "Content",
                AddedByUserId = "1",
                Image = new Image() { Id = imgId, Extension = "jpg" },
            };

            if (!dbContext.Tags.Any(x => x.Name == tag1.Name))
            {
                var tag = new Tag() { Name = tag1.Name };
                news.Tags.Add(new NewsTag { News = news, Tag = tag1 });
            }

            return news;
        }

        private void InitializeMapper() => AutoMapperConfig.
       RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

    }
}
