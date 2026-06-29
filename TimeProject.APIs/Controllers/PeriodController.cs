using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Dtos.Periods;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.ObjectValues.Records;
using TimeProject.Infrastructure.Utils;

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
    public ActionResult<IPagination<IPeriodOutDto>> Index(int recordId, [FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse(
            getPaginatedPeriodUseCase.Handle(recordId, UserClaimsUtil.Id(User), paginationQuery));
    }

    [HttpPost]
    public ActionResult<IPeriod> Create([FromBody] CreatePeriodDto data)
    {
        var result = createPeriodUseCase.Handle(data, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPost]
    [Route("list/{id:int}")]
    public ActionResult<IList<IPeriod>> Create([FromBody] PeriodListDto dto, int id)
    {
        var result = createPeriodByListUseCase.Handle(dto, id, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public ActionResult<IPeriod> Update(int id, [FromBody] PeriodDto data)
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