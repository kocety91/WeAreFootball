namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class ContactFormConfiguration : IEntityTypeConfiguration<ContactForm>
    {
        public void Configure(EntityTypeBuilder<ContactForm> contactForm)
        {
            contactForm.Property(x => x.FullName)
                 .HasMaxLength(ModelValidation.ContactForm.FullNameMaxLeght)
                 .IsRequired();

            contactForm.Property(x => x.Email)
                .IsRequired();

            contactForm.Property(x => x.Content)
                .HasMaxLength(ModelValidation.ContactForm.ContentMaxLeght)
                .IsRequired();
        }
    }
}
