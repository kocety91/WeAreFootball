namespace WeAreFootball.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
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
        private readonly string imageExtension = "test.jpg";
        private readonly string imageExtension2 = "test2.jpg";

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

            var service = new LeaguesService(repository, secondRepository);

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

        [Fact]
        public async Task CreateAsyncShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var input = this.SeedInputModel(this.imageExtension, this.imageExtension2);
            await service.CreateAsync(input, "path", "path2");

            var expected = this.SeedDataBaseLeauge(1, "fake name", "fake country", "1", "2", dbContext);

            var actual = await dbContext.Leagues.FirstOrDefaultAsync();
            var count = await dbContext.Leagues.CountAsync();

            Assert.IsType<League>(actual);
            Assert.Equal(expected.Country, actual.Country);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task CreateAsyncShouldThrowNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<League>(dbContext);
            using var secondRepository = new EfRepository<NewsLeague>(dbContext);

            var service = new LeaguesService(repository, secondRepository);

            var viewModel = this.SeedInputModel("test.pdf", "test2.pdf");
            await service.CreateAsync(viewModel, "path", "path2");

            var exception = await Assert
                .ThrowsAsync<NullReferenceException>(async () => await service.CreateAsync(viewModel, "path", "path2"));
            Assert.Equal(string.Format(ExceptionMessages.LeagueAlreadyExists, "fake name"), exception.Message);
        }

        private CreateLeagueViewModel SeedInputModel(string extension, string extension2)
        {
            var name = "fake name";
            var country = "fake country";
            var fileName = extension;
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(country);
            writer.Flush();
            stream.Position = 0;

            return new CreateLeagueViewModel
            {
                Name = name,
                Country = country,
                Image = new FormFile(stream, 0, stream.Length, "createLeague", fileName),
                Logo = new FormFile(stream, 0, stream.Length, "createLeague", fileName),
            };
        }

        private League SeedDataBaseLeauge(int id, string name, string country, string imgId, string logoId, ApplicationDbContext dbContext)
        {
            var league = new League()
            {
                Id = id,
                Name = name,
                Country = country,
                Image = new Image() { Id = imgId, Extension = "jpg" },
                Logo = new Logo() { Id = logoId, Extension = "jpg" },
            };

            if (!dbContext.Leagues.Any(x => x.Name == name))
            {
                var currentLeague = league;
                dbContext.Leagues.Add(currentLeague);
            }

            return league;
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
