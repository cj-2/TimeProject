using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Application.UseCases.Categories;

public class GetAllCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : IGetAllCategoryUseCase
{
    public ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData)
    {
        return new CustomResult<IList<CategoryOutDto>> { Data = mapper.Handle(repository.Index(userId, onlyWithData)) };
    }
}