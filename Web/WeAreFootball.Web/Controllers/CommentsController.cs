namespace WeAreFootball.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WeAreFootball.Data.Models;
    using WeAreFootball.Services.Data.Contracts;
    using WeAreFootball.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreeateCommentViewModel input)
        {
            var parentId =
                 input.ParentId == 0 ?
                     (int?)null :
                     input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, input.NewsId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.NewsId, userId, input.Content, parentId);
            return this.RedirectToAction("ById", "News", new { id = input.NewsId });
        }
    }
}
