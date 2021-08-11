namespace WeAreFootball.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using WeAreFootball.Data;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Data.Repositories;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels;
    using Xunit;

    public class VotesServiceTests
    {
        public VotesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task SetVotesWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("TestDataBase").Options;

            var votes = new List<Vote>
            {
                new Vote() { UserId = "1", Value = 1, EventId = 1 },
                new Vote() { UserId = "1", Value = 2, EventId = 1 },
                new Vote() { UserId = "1", Value = 3, EventId = 1 },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Votes.AddRangeAsync(votes);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Vote>(dbContext);

            var service = new VotesService(repository);

            var actual = service.SetVoteAsync(1, "1", 3);

            Assert.Equal(3, votes.Count);
            Assert.Equal(3, votes.First().Value);
        }

        [Fact]
        public async Task AverageVoteShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("TestDataBase").Options;

            var votes = new List<Vote>
            {
                new Vote() { UserId = "1", Value = 1, EventId = 1 },
                new Vote() { UserId = "2", Value = 2, EventId = 1 },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Votes.AddRangeAsync(votes);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Vote>(dbContext);

            var service = new VotesService(repository);

            var actual = service.GetAverageVotes(1);

            Assert.Equal(2, votes.Count);
            Assert.Equal(1.5, actual);
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
