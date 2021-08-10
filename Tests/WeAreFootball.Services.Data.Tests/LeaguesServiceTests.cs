namespace WeAreFootball.Services.Data.Tests
{
    using System.Reflection;
    using System.Threading.Tasks;
    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels;
    using Xunit;

    public class LeaguesServiceTests
    {
        public LeaguesServiceTests()
        {
            this.InitializeMapper();
        }

        [Theory]
        [InlineData("Serie A", "Italy")]
        public async Task AddLeagueShoudWorkCorrect(string name , string country)
        {

        }

        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
