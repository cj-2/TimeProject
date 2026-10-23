using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Modules.Categories.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Categories.UseCases;

public class GetAllCategoryUseCase(ICategoryRepository repository, ICategoryMapDataUtil mapper) : IGetAllCategoryUseCase
{
    public ICustomResult<IList<CategoryOutDto>> Handle(int userId, bool onlyWithData)
    {
        return new CustomResult<IList<CategoryOutDto>> { Data = mapper.Handle(repository.Index(userId, onlyWithData)) };
    }
}