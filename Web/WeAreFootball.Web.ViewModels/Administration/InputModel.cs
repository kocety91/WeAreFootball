namespace WeAreFootball.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class InputModel
    {
        [Display(Name = "Upload image")]
        public IFormFile Image { get; set; }

        public string Username { get; set; }
    }
}
