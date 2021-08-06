namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static WeAreFootball.Common.ModelValidation.News;

    public class CreateNewsViewModel : BaseNewsViewModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Display(Name = LeaguesDisplay)]
        public int[] LeagueIds { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Leagues { get; set; }

        [Display(Name = TagDisplay)]
        public int[] TagsIds { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Tags { get; set; }
    }
}
