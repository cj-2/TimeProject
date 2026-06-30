using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class RecordEntityConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder.ToTable("records");
        builder.HasKey(e => e.RecordId);
        builder.Property(e => e.RecordId).HasColumnName("record_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Code).HasColumnName("code");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.ExternalLink).HasColumnName("external_link");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.CategoryId).HasColumnName("category_id");

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").ValueGeneratedOnAddOrUpdate();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").ValueGeneratedOnAddOrUpdate();

        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);
        builder.HasMany(e => e.Periods).WithOne(e => e.Record);
    }
}