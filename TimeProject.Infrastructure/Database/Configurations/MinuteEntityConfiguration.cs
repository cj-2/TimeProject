using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class MinuteEntityConfiguration : IEntityTypeConfiguration<Minute>
{
    public void Configure(EntityTypeBuilder<Minute> builder)
    {
        builder.ToTable("minutes");
        builder.HasKey(e => e.MinuteId);
        builder.Property(e => e.MinuteId).HasColumnName("minute_id");
        builder.Property(e => e.Date).HasColumnName("date")
            .HasConversion(v => v.ToUniversalTime(), v => v.ToUniversalTime());
        builder.Property(e => e.Total).HasColumnName("total");
        builder.Property(e => e.RecordId).HasColumnName("record_id");
        builder.Property(e => e.SessionId).HasColumnName("session_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
        builder.HasOne<Record>(e => e.Record).WithMany().HasForeignKey(e => e.RecordId);
    }
}