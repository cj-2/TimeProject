using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class ConfirmCodeEntityConfiguration : IEntityTypeConfiguration<ConfirmCode>
{
    public void Configure(EntityTypeBuilder<ConfirmCode> builder)
    {
        builder.ToTable("confirm_codes");
        builder.HasKey(e => e.CodeId);
        builder.Property(e => e.CodeId).HasColumnName("code_id");
        builder.Property(e => e.Expiration).HasColumnName("expiration");
        builder.Property(e => e.IsUsed).HasColumnName("is_used");
        builder.Property(e => e.WasSent).HasColumnName("was_sent");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Type).HasColumnName("type");
        builder.HasOne<User>(e => e.User).WithMany().HasForeignKey(e => e.UserId);
    }
}