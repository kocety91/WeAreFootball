namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class LogoConfiguration : IEntityTypeConfiguration<Logo>
    {
        public void Configure(EntityTypeBuilder<Logo> logo)
        {
            logo
              .HasOne(l => l.Team)
              .WithOne(t => t.Logo)
              .HasForeignKey<Logo>(l => l.TeamId)
              .OnDelete(DeleteBehavior.Restrict);

            logo
                .HasOne(l => l.League)
                .WithOne(x => x.Logo)
                .HasForeignKey<Logo>(x => x.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
