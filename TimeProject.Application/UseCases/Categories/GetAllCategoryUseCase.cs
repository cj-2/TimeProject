using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Categories;

public class GetAllCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : IGetAllCategoryUseCase
{
    public ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData)
    {
        return new CustomResult<IList<CategoryOutDto>> { Data = mapper.Handle(repository.Index(userId, onlyWithData)) };
    }
}