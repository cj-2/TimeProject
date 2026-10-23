using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils.Interfaces;

public interface ICategoryMapDataUtil
{
    IList<CategoryOutDto> Handle(IList<Category> entities);
    CategoryOutDto Handle(Category entity);
}