using System.Net;
using Microsoft.AspNetCore.Mvc;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Database.Entities.Enums;

namespace TimeProject.APIs.Controllers.Shared;

public class CustomController : ControllerBase
{
    protected ActionResult<T> HandleResponse<T>(ICustomResult<T> customResult)
    {
        if (!string.IsNullOrEmpty(customResult.ActionName) && customResult.IsValid)
            return CreatedAtAction(customResult.ActionName, customResult.Data);

        if (customResult.IsValid) return Ok(customResult.Data);

        var messageSplit = customResult.Message?.Split(":") ?? ["generic_error"];
        var errorResponse = new ErrorResult { Message = messageSplit[^1] };

        var code = messageSplit[0];

        if (code.Contains("forbidden")) return Forbid();
        if (code.Contains("bad_request")) return BadRequest(errorResponse);
        if (code.Contains("not_found")) return NotFound(errorResponse);
        if (code.Contains("unauthorized")) return Unauthorized();
        if (code.Contains("server_error")) return StatusCode(500, errorResponse);

        return BadRequest(errorResponse);
    }

    protected bool IsAdmin()
    {
        return nameof(UserRoleType.Admin) == UserClaimsUtil.Role(User);
    }

    protected bool HasAuthorization(int id)
    {
        return UserClaimsUtil.Id(User) == id || IsAdmin();
    }

    public IPAddress? GetClientIpAddress(HttpContext context)
    {
        var headers = new[] { "X-Forwarded-For", "X-Real-IP", "CF-Connecting-IP" };

        foreach (var header in headers)
        {
            if (!context.Request.Headers.TryGetValue(header, out var headerValue)) continue;
            var ip = headerValue.FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim();
            if (string.IsNullOrEmpty(ip)) continue;

            if (IPAddress.TryParse(ip, out var address))
            {
                return address;
            }
        }

        return context.Connection.RemoteIpAddress;
    }
}