using System.Security.Claims;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Utils;

public static class UserClaimsUtil
{
    public static int Id(ClaimsPrincipal p)
    {
        return int.Parse(GetValue(p, "Id") ?? "-1");
    }

    public static string Name(ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Name) ?? "";
    }

    public static string Email(ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Email) ?? "";
    }

    public static bool IsVerified(ClaimsPrincipal p)
    {
        return bool.Parse(GetValue(p, "IsVerified") ?? "False");
    }

    public static string Role(ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Role) ?? UserRoleType.Normal.ToString();
    }

    private static string? GetValue(ClaimsPrincipal p, string type)
    {
        return p.Claims.FirstOrDefault(claim => claim.Type == type)?.Value ?? null;
    }
}