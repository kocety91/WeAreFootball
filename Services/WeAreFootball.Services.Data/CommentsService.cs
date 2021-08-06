namespace WeAreFootball.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using WeAreFootball.Data.Common.Repositories;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commnetsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commnetsRepository)
        {
            this.commnetsRepository = commnetsRepository;
        }

        public async Task Create(int newsId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                NewsId = newsId,
                ApplicationUserId = userId,
            };
            await this.commnetsRepository.AddAsync(comment);
            await this.commnetsRepository.SaveChangesAsync();
        }

        public bool IsInPostId(int commentId, int newsId)
        {
            var commentNewsId = this.commnetsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.NewsId).FirstOrDefault();
            return commentNewsId == newsId;
        }
    }
}
