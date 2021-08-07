namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> @event)
        {
            @event
              .HasOne(e => e.User)
              .WithMany(u => u.Events)
              .HasForeignKey(e => e.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            @event
                .HasOne(e => e.League)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.LeagueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            @event
                .HasOne(e => e.Image)
                .WithOne(i => i.Event)
                .HasForeignKey<Event>(e => e.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            @event.Property(x => x.Content)
                .IsRequired();

            @event.Property(x => x.IsDerbyOfTheWeek)
                .IsRequired();

            @event.Property(x => x.Sign)
                .IsRequired();

            @event.Property(x => x.Date)
                .IsRequired();
        }
    }
}
