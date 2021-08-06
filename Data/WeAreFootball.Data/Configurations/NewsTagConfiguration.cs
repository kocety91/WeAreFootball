namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Data.Models;

    public class NewsTagConfiguration : IEntityTypeConfiguration<NewsTag>
    {
        public void Configure(EntityTypeBuilder<NewsTag> newstag)
        {
            newstag.Property(x => x.TagId)
                .IsRequired();

            newstag.Property(x => x.NewsId)
                .IsRequired();
        }
    }
}
