using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Extensions;
using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Modules.Periods.Interfaces;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("periods")]
[Authorize(Policy = "IsActive")]
public class PeriodController(
    IGetPaginatedPeriodUseCase getPaginatedPeriodUseCase,
    ICreatePeriodUseCase createPeriodUseCase,
    ICreatePeriodByListUseCase createPeriodByListUseCase,
    IUpdatePeriodUseCase updatePeriodUseCase,
    IDeletePeriodUseCase deletePeriodUseCase
)
    : CustomController
{
    [HttpGet]
    [Route("{recordId:int}")]
    public ActionResult<Pagination<PeriodOutDto>> Index(int recordId, [FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse(
            getPaginatedPeriodUseCase.Handle(recordId, UserClaimsExtensions.Id(User), paginationQuery));
    }

    [HttpPost]
    public ActionResult<Period> Create([FromBody] CreatePeriodDto data)
    {
        var result = createPeriodUseCase.Handle(data, UserClaimsExtensions.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPost]
    [Route("list/{id:int}")]
    public ActionResult<IList<Period>> Create([FromBody] PeriodListDto dto, int id)
    {
        var result = createPeriodByListUseCase.Handle(dto, id, UserClaimsExtensions.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public ActionResult<Period> Update(int id, [FromBody] PeriodDto data)
    {
        return HandleResponse(updatePeriodUseCase.Handle(id, data, UserClaimsExtensions.Id(User)));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse(deletePeriodUseCase.Handle(id, UserClaimsExtensions.Id(User)));
    }
}