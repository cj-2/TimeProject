using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Statistics;
using TimeProject.Domain.Dtos.Statistics;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("statistics")]
[Authorize(Policy = "IsActive")]
public class StatisticController(IGetRangeDaysStatisticUseCase getRangeDaysStatisticUseCase) : CustomController
{
    [HttpGet]
    [Route("day")]
    public ActionResult<IRangeStatistic> Day([FromQuery] DateTimeOffset? date)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), date));
    }

    [HttpGet]
    [Route("{recordId:int}/day")]
    public ActionResult<IRangeStatistic> Day(int recordId, [FromQuery] DateTimeOffset? date)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), date, null, recordId));
    }

    [HttpGet]
    [Route("range")]
    public ActionResult<IRangeStatisticsWithDays> Range([FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), start, end));
    }
}