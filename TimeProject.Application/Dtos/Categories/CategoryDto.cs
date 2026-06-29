using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Categories;

public class CategoryDto
{
    [MaxLength(20)] public string Name { get; set; } = string.Empty;
}