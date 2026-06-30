using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class UserProviderEntityConfiguration : IEntityTypeConfiguration<UserProvider>
{
    public void Configure(EntityTypeBuilder<UserProvider> builder)
    {
        builder.ToTable("user_providers");
        builder.HasKey(e => e.ProviderId);
        builder.Property(e => e.ProviderId).HasColumnName("provider_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Provider).HasColumnName("provider");
        builder.Property(e => e.ExternalId).HasColumnName("external_id");
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
    }
}