namespace WeAreFootball.Data.Models
{
    using System.Collections.Generic;

    using WeAreFootball.Data.Common.Models;

    public class League : BaseDeletableModel<int>
    {
        public League()
        {
            this.Teams = new HashSet<Team>();
            this.News = new HashSet<NewsLeague>();
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string LogoId { get; set; }

        public virtual Logo Logo { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<NewsLeague> News { get; set; }
    }
}
