using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Extensions;
using TimeProject.Application.Modules.Sessions.Interfaces;
using TimeProject.Application.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("sessions")]
[Authorize(Policy = "IsActive")]
public class SessionController(IDeleteSessionUseCase deleteSessionUseCase) : CustomController
{
    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult<bool> Delete(int id)
    {
        return HandleResponse(deleteSessionUseCase.Handle(id, UserClaimsExtensions.Id(User)));
    }
}