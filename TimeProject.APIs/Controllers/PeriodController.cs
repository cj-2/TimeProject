using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.UseCases.Periods;
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
            getPaginatedPeriodUseCase.Handle(recordId, UserClaimsUtil.Id(User), paginationQuery));
    }

    [HttpPost]
    public ActionResult<Period> Create([FromBody] CreatePeriodDto data)
    {
        var result = createPeriodUseCase.Handle(data, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPost]
    [Route("list/{id:int}")]
    public ActionResult<IList<Period>> Create([FromBody] PeriodListDto dto, int id)
    {
        var result = createPeriodByListUseCase.Handle(dto, id, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public ActionResult<Period> Update(int id, [FromBody] PeriodDto data)
    {
        return HandleResponse(updatePeriodUseCase.Handle(id, data, UserClaimsUtil.Id(User)));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse(deletePeriodUseCase.Handle(id, UserClaimsUtil.Id(User)));
    }
}