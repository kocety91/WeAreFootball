namespace WeAreFootball.Web.ViewModels.Teams
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static WeAreFootball.Common.ModelValidation;
    using static WeAreFootball.Common.ModelValidation.Team;

    public abstract class BaseTeamViewModel
    {
        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght, ErrorMessage = NameLengthError)]
        [Display(Name = NameDisplay)]
        public string Name { get; set; }

        [Required]
        [StringLength(CityNameLenght, MinimumLength = CityMinNameLenght, ErrorMessage = NameLengthError)]
        [Display(Name = CityDisplay)]
        public string City { get; set; }

        [Required]
        [StringLength(StadiumMaxName, MinimumLength = StadiumMinName, ErrorMessage = NameLengthError)]
        [Display(Name = StadiumNameDisplay)]
        public string StadiumName { get; set; }

        [Required]
        [Display(Name = LeaguesDisplay)]
        public int LeagueId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LeagueItems { get; set; }
    }
}
