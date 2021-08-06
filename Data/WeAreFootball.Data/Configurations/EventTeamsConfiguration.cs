namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class EventTeamsConfiguration : IEntityTypeConfiguration<EventTeams>
    {
        public void Configure(EntityTypeBuilder<EventTeams> eventTeams)
        {
            eventTeams.Property(x => x.EventId)
                .IsRequired();

            eventTeams.Property(x => x.TeamId)
                .IsRequired();
        }
    }
}
