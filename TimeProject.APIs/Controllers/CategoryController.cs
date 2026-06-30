using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Dtos.General;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("categories")]
[Authorize(Policy = "IsActive")]
public class CategoryController(
    IGetAllCategoryUseCase getAllCategoryUseCase,
    IGetPaginatedCategoryUseCase getPaginatedCategoryUseCase,
    ICreateCategoryUseCase createCategoryUseCase,
    IDeleteCategoryUseCase deleteCategoryUseCase,
    IUpdateCategoryUseCase updateCategoryUseCase
)
    : CustomController
{
    [HttpGet]
    public ActionResult<IPagination<CategoryOutDto>> Index([FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse( getPaginatedCategoryUseCase.Handle(paginationQuery, UserClaimsUtil.Id(User)));
    }

    [HttpGet]
    [Route("all")]
    public ActionResult<IList<CategoryOutDto>> Index([FromQuery] bool onlyWithData)
    {
        return HandleResponse(getAllCategoryUseCase.Handle(UserClaimsUtil.Id(User), onlyWithData));
    }

    [HttpPost]
    public ActionResult<CategoryOutDto> Create([FromBody] CategoryDto dto)
    {
        var result =  createCategoryUseCase.Handle(dto, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public ActionResult<Category> Update(int id, [FromBody] CategoryDto dto)
    {
        return HandleResponse( updateCategoryUseCase.Handle(id, dto, UserClaimsUtil.Id(User)));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse( deleteCategoryUseCase.Handle(id, UserClaimsUtil.Id(User)));
    }
}