namespace WeAreFootball.Data.Models
{
    using System;

    using WeAreFootball.Data.Common.Models;

    public class Logo : BaseDeletableModel<string>
    {
        public Logo()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public int? LeagueId { get; set; }

        public virtual League League { get; set; }
    }
}
