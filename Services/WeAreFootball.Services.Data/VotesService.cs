namespace WeAreFootball.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int eventId)
        {
            return this.votesRepository.All()
                .Where(x => x.EventId == eventId).Average(x => x.Value);
        }

        public async Task SetVoteAsync(int eventId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                 .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote()
                {
                    EventId = eventId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
