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
    using WeAreFootball.Web.ViewModels.Leagues;
    using Xunit;

    public class LeaguesServiceTests
    {
        public LeaguesServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task DeleteShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League()
            {
                Id = 1,
                Name = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository,secondRepository);

            await service.DeleteAsync(1);

            var actual = await dbContext.Leagues.CountAsync();

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteShouldThrowNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League()
            {
                Id = 1,
                Name = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);
            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteAsync(2));
            Assert.Equal(string.Format(ExceptionMessages.LeagueDoesntExists), exception.Message);
        }

        [Fact]
        public async Task GetByIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League()
            {
                Id = 1,
                Name = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var expected = new LeagueViewModel()
            {
                Id = 1,
                Name = "RandomName",
            };

            var actual = service.GetById<LeagueViewModel>(1);
            Assert.Equal(JsonConvert.SerializeObject(expected.Name), JsonConvert.SerializeObject(actual.Name));
        }

        [Fact]
        public async Task GetCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League { Id = 1, Name = "RandomName" };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var result = service.GetCount();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<League>()
            {
               new League { Id = 1, Name = "Leaue1" },
               new League { Id = 2, Name = "Leaue2" },
               new League { Id = 3, Name = "Leaue3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var result = service.GetAll<LeagueViewModel>();
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task AllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<League>()
            {
               new League { Id = 1, Name = "Leaue1" },
               new League { Id = 2, Name = "Leaue2" },
               new League { Id = 3, Name = "Leaue3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var result = service.All<LeagueViewModel>();
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task ShowAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<League>()
            {
               new League { Id = 1, Name = "Leaue1" },
               new League { Id = 2, Name = "Leaue2" },
               new League { Id = 3, Name = "Leaue3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var result = service.ShowAll<LeagueViewModel>(1, 6);
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetLeagueNameShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var league = new League { Id = 1, Name = "RandomName" };
            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var expected = new LeagueViewModel()
            {
                Id = 1,
                Name = "RandomName",
            };

            var actual = service.GetLeagueName(1);

            Assert.Equal(JsonConvert.SerializeObject(expected.Name), JsonConvert.SerializeObject(actual.ToString()));
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
