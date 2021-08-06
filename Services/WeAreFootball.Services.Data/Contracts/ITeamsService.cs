namespace WeAreFootball.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        int GetCount();

        T GetById<T>(int id);

        T GetByTagName<T>(string tagName);

        IEnumerable<T> GetByLeagueId<T>(int leagueId);

        IEnumerable<T> All<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        Task<int> CreateAsync(CreateTeamViewModel input, string imagePath, string imagePathLogo);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task DeleteAsync(int id);
    }
}
