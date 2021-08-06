namespace WeAreFootball.Web.ViewModels.Newses
{
    using System.ComponentModel.DataAnnotations;

    using static WeAreFootball.Common.ModelValidation;
    using static WeAreFootball.Common.ModelValidation.News;

    public abstract class BaseNewsViewModel
    {
        [Required]
        [StringLength(TitleMaxLenght, MinimumLength = TitleMinLenght, ErrorMessage = NameLengthError)]
        [Display(Name = TitleDisplay)]
        public string Title { get; set; }

        [Required]
        [Display(Name = ContentDisplay)]
        public string Content { get; set; }
    }
}
