namespace WeAreFootball.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int eventId, string userId, byte value);

        double GetAverageVotes(int eventId);
    }
}
