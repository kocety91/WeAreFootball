namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> news)
        {
            news
                .HasOne(n => n.Image)
                .WithOne(i => i.News)
                .HasForeignKey<News>(n => n.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            news
                .HasOne(n => n.AddedByUser)
                .WithMany(i => i.CreatedNews)
                .HasForeignKey(n => n.AddedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            news.Property(x => x.Title)
                .HasMaxLength(ModelValidation.News.TitleMaxLenght)
                .IsRequired();

            news.Property(x => x.Content)
                .IsRequired();
        }
    }
}
