using AutoMapper;
using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class CategoryMapDataUtil(IMapper mapper) : ICategoryMapDataUtil
{
    public IList<CategoryOutDto> Handle(IList<ICategory> entities)
    {
        return mapper.Map<IList<ICategory>, IList<CategoryOutDto>>(entities);
    }

    public CategoryOutDto Handle(ICategory entity)
    {
        return mapper.Map<ICategory, CategoryOutDto>(entity);
    }
}