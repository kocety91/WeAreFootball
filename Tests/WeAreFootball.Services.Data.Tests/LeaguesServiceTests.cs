namespace WeAreFootball.Services.Data.Tests
{
    using System.Reflection;

    using WeAreFootball.Services.Mapping;
    using WeAreFootball.Web.ViewModels;

    public class LeaguesServiceTests
    {
        public LeaguesServiceTests()
        {
            this.InitializeMapper();
        }




        private void InitializeMapper() => AutoMapperConfig.
          RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
    }
}
