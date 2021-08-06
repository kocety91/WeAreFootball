namespace WeAreFootball.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WeAreFootball.Web.ViewModels.Leagues;

    public interface ILeaguesService
    {
        int GetCount();

        T GetById<T>(int id);

        string GetLeagueName(int leagueId);

        IEnumerable<T> All<T>();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> ShowAll<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetleaguesForNews<T>(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task<int> CreateAsync(CreateLeagueViewModel input, string imagePath, string imagePathLogo);

        Task DeleteAsync(int id);
    }
}
