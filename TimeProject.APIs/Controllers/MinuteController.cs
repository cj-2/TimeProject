using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Extensions;
using TimeProject.Application.Modules.Minutes.Dtos;
using TimeProject.Application.Modules.Minutes.Interfaces;
using TimeProject.Application.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("minutes")]
[Authorize(Policy = "IsActive")]
public class MinuteController(
    ICreateMinuteByListUseCase createMinuteByListUseCase,
    IDeleteMinuteUseCase deleteMinuteUseCase) : CustomController
{
    [HttpPost]
    [Route("list/{recordId:int}")]
    public ActionResult<List<CreateMinuteByListOutDto>> Create([FromRoute] int recordId,
        [FromBody] CreateMinuteByListDto dto)
    {
        dto.RecordId = recordId;
        var result = createMinuteByListUseCase.Handle(dto, UserClaimsExtensions.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = deleteMinuteUseCase.Handle(id, UserClaimsExtensions.Id(User));
        return HandleResponse(result);
    }
}