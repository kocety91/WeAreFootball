namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class NewsLeagueConfiguration : IEntityTypeConfiguration<NewsLeague>
    {
        public void Configure(EntityTypeBuilder<NewsLeague> newsLeague)
        {
            newsLeague.Property(x => x.LeagueId)
                .IsRequired();

            newsLeague.Property(x => x.NewsId)
                .IsRequired();
        }
    }
}
