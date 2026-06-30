using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class UserPasswordEntityConfiguration : IEntityTypeConfiguration<UserPassword>
{
    public void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        builder.ToTable("user_passwords");
        builder.HasKey(e => e.PasswordId);
        builder.Property(e => e.PasswordId).HasColumnName("password_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Password).HasColumnName("password");
        builder.Property(e => e.IsActive).HasColumnName("is_active");
        builder.HasOne<User>().WithOne().HasForeignKey<UserPassword>(e => e.UserId);
    }
}