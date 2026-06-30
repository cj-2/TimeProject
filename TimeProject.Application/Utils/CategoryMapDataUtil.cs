using AutoMapper;
using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class CategoryMapDataUtil(IMapper mapper) : ICategoryMapDataUtil
{
    public IList<CategoryOutDto> Handle(IList<Category> entities)
    {
        return mapper.Map<IList<Category>, IList<CategoryOutDto>>(entities);
    }

    public CategoryOutDto Handle(Category entity)
    {
        return mapper.Map<Category, CategoryOutDto>(entity);
    }
}