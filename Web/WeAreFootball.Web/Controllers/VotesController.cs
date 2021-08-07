namespace WeAreFootball.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseViewModel>> Post(PostVoteViewModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.EventId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.EventId);
            return new PostVoteResponseViewModel { AverageVote = averageVotes };
        }
    }
}
