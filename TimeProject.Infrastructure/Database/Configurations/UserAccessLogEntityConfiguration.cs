using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class UserAccessLogEntityConfiguration : IEntityTypeConfiguration<UserAccessLog>
{
    public void Configure(EntityTypeBuilder<UserAccessLog> builder)
    {
        builder.ToTable("user_access_logs");
        builder.HasKey(e => e.LogId);
        builder.Property(e => e.LogId).HasColumnName("log_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.ClientIp).HasColumnName("client_ip");
        builder.Property(e => e.UserAgent).HasColumnName("user_agent");
        builder.Property(e => e.Type).HasColumnName("type");
        builder.Property(e => e.Provider).HasColumnName("provider");
        builder.Property(e => e.AccessedAt).HasColumnName("accessed_at");

        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
    }
}