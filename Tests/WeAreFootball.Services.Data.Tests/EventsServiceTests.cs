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
    using WeAreFootball.Web.ViewModels.Events;
    using Xunit;

    public class EventsServiceTests
    {
        public EventsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task DeleteShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @event = new Event()
            {
                Id = 1,
                Content = "Random",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, repository2);

            await service.DeleteAsync(1);

            var actual = await dbContext.Events.CountAsync();

            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task DeleteShouldThrowNullReferenceException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @event = new Event()
            {
                Id = 1,
                Content = "Random",
            };

            using var dbContext = new ApplicationDbContext(options);
            await dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var repository2 = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, repository2);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteAsync(2));
            Assert.Equal(string.Format(ExceptionMessages.EventDoesntExists), exception.Message);
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var list = new List<Event>()
            {
               new Event { Id = 1, Content = "Event1" },
               new Event { Id = 2, Content = "Event2" },
               new Event { Id = 3, Content = "Event3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddRangeAsync(list);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var result = service.GetAll<EventTestViewModel>(1, 6);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @event = new Event()
            {
                Id = 1,
                Content = "RandomName",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var expected = new EventTestViewModel()
            {
                Id = 1,
                Content = "RandomName",
            };

            var actual = service.GetById<EventTestViewModel>(1);
            Assert.Equal(JsonConvert.SerializeObject(expected.Content), JsonConvert.SerializeObject(actual.Content));
        }

        [Fact]
        public async Task GetCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @event = new Event { Id = 1, Content = "Random" };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var result = service.GetCount();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task TodayEventsShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var today = DateTime.UtcNow;
            DateTime dateValue;
            var yesterday = DateTime.TryParse("1999-08-08", out dateValue);

            var @events = new List<Event>()
            {
                new Event() { Id = 1, Date = today, Content = "Conten1" },
                new Event() { Id = 2,  Date = dateValue, Content = "Conten2" },
                new Event() { Id = 3, Date = dateValue, Content = "Conten3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddRangeAsync(events);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var result = service.TodayEvents<EventTestViewModel>();

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task GetLatestDerbyOfTheWeekShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @events = new List<Event>()
            {
                new Event() { Id = 1, Content = "Conten1", IsDerbyOfTheWeek = true },
                new Event() { Id = 2, Content = "Conten2", IsDerbyOfTheWeek = true },
                new Event() { Id = 3, Content = "Conten3", IsDerbyOfTheWeek = true },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddRangeAsync(events);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var actual = service.GetLatestDerbyOfTheWeek<EventTestViewModel>();

            var expeced = new Event() { Id = 3, Content = "Conten3", IsDerbyOfTheWeek = true };

            Assert.Equal(JsonConvert.SerializeObject(expeced.Id), JsonConvert.SerializeObject(actual.Id));

        }

        [Fact]
        public async Task GetByLeagueIdShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var @events = new List<Event>()
            {
                new Event() { Id = 1, Content = "Conten1", LeagueId = 1 },
                new Event() { Id = 2, Content = "Conten2", LeagueId = 1 },
                new Event() { Id = 3, Content = "Conten3", LeagueId = 1 },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.Events.AddRangeAsync(events);
            await dbContext.SaveChangesAsync();

            using var repository = new EfRepository<Event>(dbContext);
            using var secondRepository = new EfDeletableEntityRepository<Team>(dbContext);

            var service = new EventsService(repository, secondRepository);

            var actual = service.GetByLeagueId<EventTestViewModel>(1, 1, 6);

            Assert.Equal(3, actual.Count());
        }

        private void InitializeMapper() => AutoMapperConfig.
         RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
