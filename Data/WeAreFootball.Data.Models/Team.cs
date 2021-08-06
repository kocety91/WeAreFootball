namespace WeAreFootball.Data.Models
{
    using System.Collections.Generic;

    using WeAreFootball.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.EventTeams = new HashSet<EventTeams>();
        }

        public string Name { get; set; }

        public string City { get; set; }

        public string StadiumName { get; set; }

        public string LogoId { get; set; }

        public virtual Logo Logo { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }

        public virtual ICollection<EventTeams> EventTeams { get; set; }
    }
}
