namespace WeAreFootball.Web.ViewModels.Leagues
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateLeagueViewModel : BaseLeagueViewModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public IFormFile Logo { get; set; }
    }
}
