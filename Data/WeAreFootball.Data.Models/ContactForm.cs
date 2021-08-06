namespace WeAreFootball.Data.Models
{
    using WeAreFootball.Data.Common.Models;

    public class ContactForm : BaseModel<int>
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }
    }
}