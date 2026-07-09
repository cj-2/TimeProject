using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");
        builder.HasKey(e => e.SessionId);
        builder.Property(e => e.SessionId).HasColumnName("session_id");
        builder.Property(e => e.Type).HasColumnName("type");
        builder.Property(e => e.Date).HasColumnName("date")
            .HasConversion(v => v.ToUniversalTime(), v => v.ToUniversalTime());
        builder.Property(e => e.From).HasColumnName("session_from");
        builder.Property(e => e.RecordId).HasColumnName("record_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
        builder.HasOne<Record>(e => e.Record).WithMany().HasForeignKey(e => e.RecordId);
        builder.HasOne<Category>(e => e.Category).WithMany().HasForeignKey(e => e.RecordId);
        builder.HasMany(e => e.Periods).WithOne(e => e.Session).HasForeignKey(e => e.SessionId);
    }
}