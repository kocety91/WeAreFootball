namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class EventTeams : BaseModel<int>
    {
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
