using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Domain.Dtos.Codes;
using TimeProject.Infrastructure.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("codes")]
public class ConfirmCodeController(IGetRegisterCodeInfoUseCase getRegisterCodeInfoUseCase) : CustomController
{
    [HttpGet]
    [Route("register/info")]
    [Authorize]
    public ActionResult<IConfirmCodeOutDto> HasVerifyCodeActive()
    {
        return HandleResponse(getRegisterCodeInfoUseCase.Handle(UserClaimsUtil.Id(User)));
    }
}