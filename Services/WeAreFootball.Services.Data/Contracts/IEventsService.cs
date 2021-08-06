namespace WeAreFootball.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventsService
    {
        int GetCount();

        T GetLatestDerbyOfTheWeek<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetByTeamId<T>(int teamId);

        IEnumerable<T> GetByLeagueId<T>(int leagueId, int page, int itemsPerPage = 6);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetByTeamName<T>(string teamName);

        IEnumerable<T> GetLastFour<T>();

        Task<int> CreateAsync(CreateEventViewModel input, string userId, string imagePath);

        IEnumerable<T> TodayEvents<T>();

        Task DeleteAsync(int id);
    }
}
