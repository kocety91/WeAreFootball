namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> league)
        {
            league.HasOne(l => l.Image)
                .WithOne(i => i.League)
                .HasForeignKey<Image>(i => i.LeagueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            league.HasOne(l => l.Logo)
                .WithOne(l => l.League)
                .HasForeignKey<Logo>(l => l.LeagueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            league.Property(x => x.Name)
                .HasMaxLength(ModelValidation.League.NameMaxLenght)
                .IsRequired();

            league.Property(x => x.Country)
                .HasMaxLength(ModelValidation.League.CountryNameLenght)
                .IsRequired();
        }
    }
}
