namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int? NewsId { get; set; }

        public virtual News News { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
