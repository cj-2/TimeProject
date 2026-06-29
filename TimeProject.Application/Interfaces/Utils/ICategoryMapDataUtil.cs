using TimeProject.Application.Dtos.Categories;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface ICategoryMapDataUtil
{
    IList<CategoryOutDto> Handle(IList<ICategory> entities);
    CategoryOutDto Handle(ICategory entity);
}