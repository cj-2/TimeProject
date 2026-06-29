using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Application.UseCases.Categories;

public class CreateCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : ICreateCategoryUseCase
{
    public ICustomResult<ICategoryOutDto> Handle(ICategoryDto dto, int userId)
    {
        var result = new CustomResult<ICategoryOutDto>();
        var category = repository.FindByName(dto.Name, userId);

        if (category != null)
            return result.SetData(mapper.Handle(category));

        result.Data = mapper.Handle(repository.Create(new Category { UserId = userId, Name = dto.Name }));
        return result;
    }
}