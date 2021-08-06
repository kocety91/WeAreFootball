namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
               .HasMany(e => e.Events)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
              .HasMany(e => e.Votes)
               .WithOne()
               .HasForeignKey(e => e.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            appUser
              .HasMany(e => e.Comments)
               .WithOne()
               .HasForeignKey(e => e.ApplicationUserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            appUser
             .HasMany(e => e.CreatedNews)
              .WithOne()
              .HasForeignKey(e => e.AddedByUserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasOne(e => e.Image)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey<Image>(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
