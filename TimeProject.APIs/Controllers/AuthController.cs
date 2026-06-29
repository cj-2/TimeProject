using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Database.Entities.Enums;
using TimeProject.Infrastructure.ObjectValues.Auths;
using TimeProject.APIs.Controllers.Attributes;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(
    ILoginUseCase loginUseCase,
    ILoginGithubUseCase loginGithubUseCase,
    ILoginGoogleUseCase loginGoogleUseCase
) : CustomController
{
    [HttpPost, Route("login")]
    // [UserChallenge]
    public ActionResult<JwtResult> Login([FromBody] LoginDto dto)
    {
        return HandleResponse(loginUseCase.Handle(dto, new UserAccessLog
        {
            ClientIp = GetClientIpAddress(HttpContext),
            UserAgent = Request.Headers.UserAgent.ToString(),
            Type = AccessType.Password
        }));
    }

    [HttpPost]
    [Route("login/github")]
    public async Task<ActionResult<JwtResult>> LoginGithub([FromBody] LoginGithubDto dto)
    {
        return HandleResponse(await loginGithubUseCase.Handle(dto, new UserAccessLog
        {
            ClientIp = GetClientIpAddress(HttpContext),
            UserAgent = Request.Headers.UserAgent.ToString(),
            Type = AccessType.Provider,
            Provider = ProviderType.Github
        }));
    }

    [HttpPost]
    [Route("login/google")]
    public async Task<ActionResult<JwtResult>> LoginGoogle([FromBody] LoginGoogleDto dto)
    {
        return HandleResponse(await loginGoogleUseCase.Handle(dto, new UserAccessLog
        {
            ClientIp = GetClientIpAddress(HttpContext),
            UserAgent = Request.Headers.UserAgent.ToString(),
            Type = AccessType.Provider,
            Provider = ProviderType.Google
        }));
    }
}