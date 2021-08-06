namespace WeAreFootball.Data.Models
{
    using System;
    using System.Collections.Generic;

    using WeAreFootball.Data.Common.Models;

    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Votes = new HashSet<Vote>();
            this.EventTeams = new HashSet<EventTeams>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int? LeagueId { get; set; }

        public virtual League League { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string Sign { get; set; }

        public bool IsDerbyOfTheWeek { get; set; }

        public virtual ICollection<EventTeams> EventTeams { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
