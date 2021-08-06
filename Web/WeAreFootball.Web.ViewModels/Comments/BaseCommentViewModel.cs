namespace WeAreFootball.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static WeAreFootball.Common.ModelValidation;
    using static WeAreFootball.Common.ModelValidation.Comment;

    public abstract class BaseCommentViewModel
    {
        public int NewsId { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        [StringLength(ContentMaxValue, MinimumLength = ContentMinValue, ErrorMessage = NameLengthError)]
        [Display(Name = ContentDisplay)]
        public string Content { get; set; }
    }
}
