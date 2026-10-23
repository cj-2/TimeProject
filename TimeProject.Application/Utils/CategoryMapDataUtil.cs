using AutoMapper;
using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Utils.Interfaces;
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