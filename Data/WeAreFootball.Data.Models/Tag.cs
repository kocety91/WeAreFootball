namespace WeAreFootball.Data.Models
{
    using System.Collections.Generic;

    using WeAreFootball.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.News = new HashSet<NewsTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<NewsTag> News { get; set; }
    }
}
