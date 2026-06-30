using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeProject.Domain.Entities;

namespace TimeProject.Infrastructure.Database.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(e => e.CategoryId);
        builder.Property(e => e.CategoryId).HasColumnName("category_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        
        builder.HasOne<User>().WithMany().HasForeignKey(e => e.UserId);
    }
}