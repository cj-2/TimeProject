using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class CreateCategoryUseCase(ICategoryRepository repository) : ICreateCategoryUseCase
{
    public ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId)
    {
        var result = new CustomResult<CategoryOutDto>();
        var category = repository.FindByName(dto.Name, userId);

        return result.SetData(category != null
            ? CategoryOutDto.Map(category)
            : CategoryOutDto.Map(repository.Create(new Category { UserId = userId, Name = dto.Name }))
        );
    }
}