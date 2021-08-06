namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
