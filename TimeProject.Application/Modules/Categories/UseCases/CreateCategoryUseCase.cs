using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class CreateCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : ICreateCategoryUseCase
{
    public ICustomResult<CategoryOutDto> Handle(CategoryDto dto, int userId)
    {
        var result = new CustomResult<CategoryOutDto>();
        var category = repository.FindByName(dto.Name, userId);

        if (category != null)
            return result.SetData(mapper.Handle(category));

        result.Data = mapper.Handle(repository.Create(new Category { UserId = userId, Name = dto.Name }));
        return result;
    }
}