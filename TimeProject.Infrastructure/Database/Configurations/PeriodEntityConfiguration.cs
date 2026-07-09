using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class PeriodEntityConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.ToTable("periods");
        builder.HasKey(e => e.PeriodId);
        builder.Property(e => e.PeriodId).HasColumnName("period_id");
        builder.Property(e => e.Start).HasColumnName("start_period")
            .HasConversion(v => v.ToUniversalTime(), v => v.ToUniversalTime());
        builder.Property(e => e.End).HasColumnName("end_period")
            .HasConversion(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? v.Value.ToUniversalTime() : v);
        builder.Property(e => e.RecordId).HasColumnName("record_id");
        builder.Property(e => e.SessionId).HasColumnName("session_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Record).WithMany(e => e.Periods).HasForeignKey(e => e.RecordId);
        builder.HasOne<Session>(e => e.Session).WithMany(e => e.Periods).HasForeignKey(e => e.SessionId);
    }
}