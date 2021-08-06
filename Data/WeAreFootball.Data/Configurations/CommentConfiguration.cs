namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment
              .HasOne(c => c.ApplicationUser)
              .WithMany(u => u.Comments)
              .HasForeignKey(c => c.ApplicationUserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            comment.Property(x => x.Content)
               .HasMaxLength(ModelValidation.Comment.ContentMaxValue)
               .IsRequired();
        }
    }
}
