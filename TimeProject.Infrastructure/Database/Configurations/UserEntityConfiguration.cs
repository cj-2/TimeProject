using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(e => e.UserId);
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.UserRole).HasColumnName("role");
        builder.Property(e => e.Timezone).HasColumnName("timezone");
        builder.Property(e => e.IsActive).HasColumnName("is_active");
    }
}