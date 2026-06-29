using AutoMapper;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Categories;

namespace TimeProject.Infrastructure.Utils;

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