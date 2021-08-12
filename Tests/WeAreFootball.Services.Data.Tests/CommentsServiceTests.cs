using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeAreFootball.Data;
using WeAreFootball.Data.Models;
using WeAreFootball.Data.Repositories;
using WeAreFootball.Services.Mapping;
using WeAreFootball.Web.ViewModels;
using Xunit;

namespace WeAreFootball.Services.Data.Tests
{
    public class CommentsServiceTests
    {
        public CommentsServiceTests()
        {
            this.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsyncShouldAddRightToDataBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentsService(repository);

            await service.Create(1, "userName", "funnyContent");

            var count = await dbContext.Comments.CountAsync();

            Assert.Equal(1, count);
        }

        private void InitializeMapper() => AutoMapperConfig.
       RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
