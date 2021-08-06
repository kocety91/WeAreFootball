namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> tag)
        {
            tag.Property(x => x.Name)
                .HasMaxLength(ModelValidation.Tag.NameMaxLenght)
                .IsRequired();
        }
    }
}
