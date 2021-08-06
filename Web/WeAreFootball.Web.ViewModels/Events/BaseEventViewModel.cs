namespace WeAreFootball.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEventViewModel
    {
        [Required]
        public int LeagueId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Sign { get; set; }

        [Required]
        public bool IsDerbyOfTheWeek { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TeamsItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LeagueItems { get; set; }
    }
}
