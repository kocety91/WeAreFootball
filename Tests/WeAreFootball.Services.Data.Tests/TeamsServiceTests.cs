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
using WeAreFootball.Web.ViewModels.Teams;
using Xunit;

namespace WeAreFootball.Services.Data.Tests
{
    public class TeamsServiceTests
    {
        public TeamsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task DeleteShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var team = new Team()
            {
                Id = 1,
                Name = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            await service.DeleteAsync(1);

            var actual = await dbContext.Leagues.CountAsync();

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteShouldThrowNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var team = new Team()
            {
                Id = 1,
                Name = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteAsync(2));
            Assert.Equal(string.Format(ExceptionMessages.TeamDoesntExists), exception.Message);
        }

        [Fact]
        public async Task GetByIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var team = new Team()
            {
                Id = 2,
                Name = "RandomName",
                City = "RandomCity",
                StadiumName = "RandomStadumName",
                LeagueId = 1,
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var expected = new TeamViewModel()
            {
                Id = 2,
                Name = "RandomName",
                City = "RandomCity",
                StadiumName = "RandomStadumName",
                LeagueId = 1,
            };

            var actual = service.GetById<TeamTestViewModel>(2);
            Assert.Equal(JsonConvert.SerializeObject(expected.Name), JsonConvert.SerializeObject(actual.Name));
        }

        [Fact]
        public async Task GetCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var team = new Team { Id = 1, Name = "RandomName" };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var result = service.GetCount();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<Team>()
            {
               new Team { Id = 1, Name = "Team1" },
               new Team { Id = 2, Name = "Team2" },
               new Team { Id = 3, Name = "Team3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var result = service.All<TeamTestViewModel>(1, 6);
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<Team>()
            {
               new Team { Id = 1, Name = "Team1" },
               new Team { Id = 2, Name = "Team2" },
               new Team { Id = 3, Name = "Team3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var result = service.GetAll<TeamTestViewModel>(1, 6);
            var count = result.Count();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetByLeagueIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<Team>()
            {
               new Team { Id = 1, Name = "Team1", City = "RandomCity1", StadiumName = "RandomStadium1", LeagueId = 3 },
               new Team { Id = 2, Name = "Team2", City = "RandomCity2", StadiumName = "RandomStadium2", LeagueId = 3 },
               new Team { Id = 3, Name = "Team3", City = "RandomCity3", StadiumName = "RandomStadium3", LeagueId = 3 },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Teams.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Team>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Tag>(dbContext);

            var service = new TeamsService(repository, secondRepository);

            var expected = new List<TeamTestViewModel>()
            {
               new TeamTestViewModel { Id = 1, Name = "Team1", City = "RandomCity1", StadiumName = "RandomStadium1", LeagueId = 3 },
               new TeamTestViewModel { Id = 2, Name = "Team2", City = "RandomCity2", StadiumName = "RandomStadium2", LeagueId = 3 },
               new TeamTestViewModel { Id = 3, Name = "Team3", City = "RandomCity3", StadiumName = "RandomStadium3", LeagueId = 3 },
            };

            var actual = service.GetByLeagueId<TeamTestViewModel>(3);
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
