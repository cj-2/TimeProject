using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class RecordResumeEntityConfiguration : IEntityTypeConfiguration<RecordResume>
{
    public void Configure(EntityTypeBuilder<RecordResume> builder)
    {
        builder.ToTable("record_resumes");
        builder.HasKey(e => e.RecordId);
        builder.Property(e => e.RecordId).HasColumnName("record_id");
        builder.Property(e => e.Formatted).HasColumnName("formatted");
        builder.Property(e => e.Seconds).HasColumnName("seconds");
        builder.Property(e => e.Count).HasColumnName("count");
        builder.Property(e => e.FirstDate).HasColumnName("first_date")
            .HasConversion(v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? v.Value.ToUniversalTime() : v);
        builder.Property(e => e.LastDate).HasColumnName("last_date")
            .HasConversion(v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? v.Value.ToUniversalTime() : v);
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne<Record>().WithOne(e => e.Resume).HasForeignKey<RecordResume>(e => e.RecordId);
    }
}