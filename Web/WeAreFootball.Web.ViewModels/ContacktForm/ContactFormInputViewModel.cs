namespace WeAreFootball.Web.ViewModels.ContacktForm
{
    using System.ComponentModel.DataAnnotations;

    using static WeAreFootball.Common.ModelValidation;
    using static WeAreFootball.Common.ModelValidation.ContactForm;

    public class ContactFormInputViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(FullNameMaxLeght, MinimumLength = FullNameMinLeght, ErrorMessage = NameLengthError)]
        [Display(Name = FullNameDisplay)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = EmailDisplay)]
        public string Email { get; set; }

        [Required]
        [StringLength(ContentMaxLeght, MinimumLength= ContentMinLeght, ErrorMessage = NameLengthError)]
        [Display(Name = ContentDisplay)]
        public string Content { get; set; }
    }
}
