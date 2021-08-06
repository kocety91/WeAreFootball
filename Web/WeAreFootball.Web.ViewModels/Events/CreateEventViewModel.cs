namespace WeAreFootball.Web.ViewModels.Events
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateEventViewModel : BaseEventViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
