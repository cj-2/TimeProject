using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Interfaces.UseCases.Minutes;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("minutes")]
[Authorize(Policy = "IsActive")]
public class MinuteController(
    ICreateMinuteByListUseCase createMinuteByListUseCase,
    IDeleteMinuteUseCase deleteMinuteUseCase) : CustomController
{
    [HttpPost]
    [Route("list")]
    public ActionResult<IList<IMinute>> Create([FromBody] CreateMinuteListDto dto)
    {
        var result = createMinuteByListUseCase.Handle(dto, UserClaimsUtil.Id(User));
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse(deleteMinuteUseCase.Handle(id, UserClaimsUtil.Id(User)));
    }
}