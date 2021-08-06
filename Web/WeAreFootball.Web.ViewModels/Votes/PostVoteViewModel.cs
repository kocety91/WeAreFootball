namespace WeAreFootball.Web.ViewModels.Votes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PostVoteViewModel
    {
        [Required]
        public int EventId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
