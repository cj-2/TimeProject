using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Dtos.Categories;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.UseCases.Categories;

public class GetAllCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : IGetAllCategoryUseCase
{
    public ICustomResult<IList<ICategoryOutDto>> Handle(int userId, bool onlyWithData)
    {
        return new CustomResult<IList<ICategoryOutDto>> { Data = mapper.Handle(repository.Index(userId, onlyWithData)) };
    }
}