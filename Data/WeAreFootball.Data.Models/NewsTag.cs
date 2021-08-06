﻿namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class NewsTag : BaseModel<int>
    {
        public int NewsId { get; set; }

        public virtual News News { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
