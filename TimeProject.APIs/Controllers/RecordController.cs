using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Dtos.General;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Utils;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("records")]
[Authorize(Policy = "IsActive")]
public class RecordController(
    IGetPaginatedRecordUseCase getPaginatedRecordUseCase,
    IGetRecordHistoryUseCase getRecordHistoryUseCase,
    ICreateRecordUseCase createRecordUseCase,
    IUpdateRecordUseCase updateRecordUseCase,
    IGetRecordByCodeUseCase getRecordByCodeUseCase,
    IDeleteRecordUseCase deleteRecordUseCase,
    ISearchRecordUseCase searchRecordUseCase
) : CustomController
{
    [HttpGet]
    public ActionResult<IPagination<RecordOutDto>> Index([FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse(getPaginatedRecordUseCase.Handle(paginationQuery, UserClaimsUtil.Id(User)));
    }

    [HttpGet]
    [Route("history/{recordId:int}")]
    public ActionResult<IPagination<RecordHistoryDayOutDto>> HistoryIndex([FromRoute] int recordId,
        [FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse(
            getRecordHistoryUseCase.Handle(recordId, UserClaimsUtil.Id(User), paginationQuery));
    }

    [HttpGet]
    [Route("search")]
    public ActionResult<IList<SearchRecordItem>> Search([FromQuery] string? value)
    {
        return HandleResponse(searchRecordUseCase.Handle(value ?? "", UserClaimsUtil.Id(User)));
    }

    [HttpPost]
    public ActionResult<RecordOutDto> Create([FromBody] CreateRecordDto dto)
    {
        var result = createRecordUseCase.Handle(dto, dto.Periods?.ToList(), UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut]
    [Route("{id:int}")]
    public ActionResult<RecordOutDto> Update(int id, [FromBody] UpdateRecordDto dto)
    {
        return HandleResponse(updateRecordUseCase.Handle(id, dto, UserClaimsUtil.Id(User)));
    }

    [HttpGet]
    [Route("{code}")]
    public ActionResult<RecordOutDto> Get(string code)
    {
        return HandleResponse(getRecordByCodeUseCase.Handle(code, UserClaimsUtil.Id(User)));
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse(deleteRecordUseCase.Handle(id, UserClaimsUtil.Id(User)));
    }
}