using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Categories;

public class CategoryDto
{
    [MaxLength(20)] public string Name { get; set; } = string.Empty;
}