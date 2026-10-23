using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Categories.Dtos;

public class CategoryDto
{
    [MaxLength(20)] public string Name { get; set; } = string.Empty;
}