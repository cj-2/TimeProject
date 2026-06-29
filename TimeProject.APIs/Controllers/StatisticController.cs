using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Statistics;
using TimeProject.Infrastructure.ObjectValues.Statistics;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("statistics")]
[Authorize(Policy = "IsActive")]
public class StatisticController(IGetRangeDaysStatisticUseCase getRangeDaysStatisticUseCase) : CustomController
{
    [HttpGet]
    [Route("day")]
    public ActionResult<RangeStatistic> Day([FromQuery] DateTimeOffset? date)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), date));
    }

    [HttpGet]
    [Route("{recordId:int}/day")]
    public ActionResult<RangeStatistic> Day(int recordId, [FromQuery] DateTimeOffset? date)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), date, null, recordId));
    }

    [HttpGet]
    [Route("range")]
    public ActionResult<RangeStatisticsWithDays> Range([FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end)
    {
        return HandleResponse(getRangeDaysStatisticUseCase.Handle(UserClaimsUtil.Id(User), start, end));
    }
}