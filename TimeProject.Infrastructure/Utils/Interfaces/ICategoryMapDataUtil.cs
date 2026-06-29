using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Infrastructure.Utils.Interfaces;

public interface ICategoryMapDataUtil
{
    IList<CategoryOutDto> Handle(IList<ICategory> entities);
    CategoryOutDto Handle(ICategory entity);
}