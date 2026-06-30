using TimeProject.Application.Dtos.Categories;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface ICategoryMapDataUtil
{
    IList<CategoryOutDto> Handle(IList<Category> entities);
    CategoryOutDto Handle(Category entity);
}