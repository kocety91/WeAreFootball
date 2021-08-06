namespace WeAreFootball.Data.Models
{
    using System;

    using WeAreFootball.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int? LeagueId { get; set; }

        public virtual League League { get; set; }

        public int? NewsId { get; set; }

        public virtual News News { get; set; }

        public int? EventId { get; set; }

        public virtual Event Event { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }
    }
}
