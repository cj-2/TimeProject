using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.APIs.Controllers.Attributes;
using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Domain.Entities;
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
    public ActionResult<JwtDto> Login([FromBody] LoginDto dto)
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
    public async Task<ActionResult<JwtDto>> LoginGithub([FromBody] LoginGithubDto dto)
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
    public async Task<ActionResult<JwtDto>> LoginGoogle([FromBody] LoginGoogleDto dto)
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