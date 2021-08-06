namespace WeAreFootball.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int newsId, string userId, string content, int? parentId = null);

        bool IsInPostId(int commentId, int newsId);
    }
}
