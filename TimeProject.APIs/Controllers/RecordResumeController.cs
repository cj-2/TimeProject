using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("records/meta")]
[Authorize(Policy = "IsAdmin")]
public class RecordResumeController(
    ISyncAllRecordResumeUseCase syncAllRecordResumeUseCase
) : CustomController
{
    [HttpPost]
    [Route("sync/all")]
    public ActionResult<IEnumerable<IRecordResume>> SyncAll()
    {
        return HandleResponse(syncAllRecordResumeUseCase.Handle());
    }
}