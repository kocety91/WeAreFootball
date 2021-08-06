namespace WeAreFootball.Data.Models
{
    using System.Collections.Generic;

    using WeAreFootball.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<NewsTag>();
            this.Leagues = new HashSet<NewsLeague>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public string Source { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<NewsLeague> Leagues { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<NewsTag> Tags { get; set; }
    }
}
