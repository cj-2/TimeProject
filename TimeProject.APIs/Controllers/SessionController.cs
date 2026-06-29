using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Sessions;
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
        return HandleResponse(deleteSessionUseCase.Handle(id, UserClaimsUtil.Id(User)));
    }
}