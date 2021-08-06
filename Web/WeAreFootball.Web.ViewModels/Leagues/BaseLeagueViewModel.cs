namespace WeAreFootball.Web.ViewModels.Leagues
{
    using System.ComponentModel.DataAnnotations;

    using static WeAreFootball.Common.ModelValidation;
    using static WeAreFootball.Common.ModelValidation.League;

    public abstract class BaseLeagueViewModel
    {
        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght, ErrorMessage = NameLengthError)]
        [Display(Name = NameDisplay)]
        public string Name { get; set; }

        [Required]
        [StringLength(CountryNameLenght, MinimumLength = CountryMinNameLenght, ErrorMessage = NameLengthError)]
        [Display(Name = CountryDisplay)]
        public string Country { get; set; }
    }
}
