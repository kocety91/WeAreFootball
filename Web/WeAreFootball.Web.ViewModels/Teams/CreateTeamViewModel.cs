namespace WeAreFootball.Web.ViewModels.Teams
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateTeamViewModel : BaseTeamViewModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public IFormFile Logo { get; set; }
    }
}
