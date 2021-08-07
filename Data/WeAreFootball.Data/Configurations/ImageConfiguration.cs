namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> image)
        {
            image
              .HasOne(i => i.ApplicationUser)
              .WithOne(u => u.Image)
              .HasForeignKey<Image>(i => i.ApplicationUserId)
              .OnDelete(DeleteBehavior.Restrict);

            image
              .HasOne(i => i.Team)
              .WithOne(t => t.Image)
              .HasForeignKey<Image>(i => i.TeamId)
              .OnDelete(DeleteBehavior.Restrict);

            image
              .HasOne(i => i.League)
              .WithOne(l => l.Image)
              .HasForeignKey<Image>(n => n.LeagueId)
              .OnDelete(DeleteBehavior.Restrict);

            image
              .HasOne(i => i.News)
              .WithOne(n => n.Image)
              .HasForeignKey<Image>(i => i.NewsId)
              .OnDelete(DeleteBehavior.Restrict);

            image
              .HasOne(i => i.Event)
              .WithOne(e => e.Image)
              .HasForeignKey<Image>(i => i.EventId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
