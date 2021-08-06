namespace WeAreFootball.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WeAreFootball.Common;
    using WeAreFootball.Data.Models;

    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> vote)
        {
            vote
                .HasOne(v => v.Event)
                .WithMany(e => e.Votes)
                .HasForeignKey(v => v.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            vote
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            vote.Property(x => x.Value)
                .HasMaxLength(ModelValidation.Vote.VoteMaxValue)
                .IsRequired();
        }
    }
}
