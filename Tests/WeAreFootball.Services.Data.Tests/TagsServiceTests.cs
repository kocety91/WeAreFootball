namespace WeAreFootball.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using WeAreFootball.Data;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Data.Repositories;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels;
    using WeAreFootball.Web.ViewModels.Newses;
    using Xunit;

    public class TagsServiceTests
    {
        public TagsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetTagsForNewsShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var tag = new Tag()
            {
                Id = 1,
                Name = "Tag1",
            };

            var news = new News()
            {
                Id = 1,
                Title = "RandomTitle",
            };

            var newsTags = new NewsTag()
            {
                Id = 1,
                News = news,
                Tag = tag,
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.NewsTags.AddRangeAsync(newsTags);
            await dbContext.SaveChangesAsync();

            using var repository1 = new EfDeletableEntityRepository<Tag>(dbContext);
            using var repository2 = new EfRepository<NewsTag>(dbContext);

            var service = new TagsService(repository1, repository2);

            var actual = service.GetTagsForNews<NewsTagViewModel>(1);

            var expected = new List<NewsTagViewModel>()
            {
               new NewsTagViewModel() { Id = 1, TagName = "Tag1" },
            };

            var count = actual.Count();

            Assert.Equal(1, count);
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
