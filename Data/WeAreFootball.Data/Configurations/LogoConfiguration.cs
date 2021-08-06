namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class LogoConfiguration : IEntityTypeConfiguration<Logo>
    {
        public void Configure(EntityTypeBuilder<Logo> logo)
        {
            logo.Property(x => x.Extension)
                .IsRequired();

            logo.Property(x => x.RemoteImageUrl)
                .IsRequired();
        }
    }
}
