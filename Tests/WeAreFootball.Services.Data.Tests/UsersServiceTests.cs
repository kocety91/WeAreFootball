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
    using WeAreFootball.Web.ViewModels.Users;
    using Xunit;

    public class UsersServiceTests
    {
        public UsersServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var user = new ApplicationUser()
            {
                Id = "1",
                Email = "abv.bg",
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.ApplicationUsers.AddAsync(user);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            var result = service.GetAll<UserViewModel>(1, 6);

            var expected = new UserViewModel()
            {
                Id = "1",
                Email = "abv.bg",
            };
            Assert.Equal(JsonConvert.SerializeObject(expected.Email), JsonConvert.SerializeObject(result.First().Email));
        }

        [Fact]
        public async Task GetCountShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var users = new List<ApplicationUser>()
            {
               new ApplicationUser { Id = "1" },
               new ApplicationUser { Id = "2" },
               new ApplicationUser { Id = "3" },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.ApplicationUsers.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            var result = service.GetCount();

            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetRegisteredUsersTodayShouldWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var today = DateTime.UtcNow;
            DateTime dateValue;
            var yesterday = DateTime.TryParse("1999-08-08", out dateValue);

            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "1", CreatedOn = today },
                new ApplicationUser() { Id = "2",  CreatedOn = dateValue },
                new ApplicationUser() { Id = "3", CreatedOn = dateValue },
            };

            using var dbContext = new ApplicationDbContext(options);

            await dbContext.ApplicationUsers.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            var result = service.GetRegisteredUsersToday(today);

            Assert.Equal(1, result);
        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
