using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Categories.Dtos;

public class CategoryOutDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public static CategoryOutDto Map(Category category)
    {
        return new CategoryOutDto { Name = category.Name };
    }
}