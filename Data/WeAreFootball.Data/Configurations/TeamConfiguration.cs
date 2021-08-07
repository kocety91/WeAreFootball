namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team.HasOne(t => t.Image)
                 .WithOne(i => i.Team)
                 .HasForeignKey<Image>(i => i.TeamId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            team.HasOne(t => t.Logo)
                .WithOne(l => l.Team)
                .HasForeignKey<Logo>(l => l.TeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            team.HasOne(t => t.League)
                .WithMany(l => l.Teams)
                .HasForeignKey(t => t.LeagueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            team.Property(x => x.Name)
                .HasMaxLength(ModelValidation.Team.NameMaxLenght)
                .IsRequired();

            team.Property(x => x.StadiumName)
                .HasMaxLength(ModelValidation.Team.StadiumMaxName)
                .IsRequired();

            team.Property(x => x.City)
                .HasMaxLength(ModelValidation.Team.CityNameLenght)
                .IsRequired();
        }
    }
}
