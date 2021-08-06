namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class NewsLeague : BaseModel<int>
    {
        public int NewsId { get; set; }

        public virtual News News { get; set; }

        public int LeagueId { get; set; }

        public virtual League League { get; set; }
    }
}
