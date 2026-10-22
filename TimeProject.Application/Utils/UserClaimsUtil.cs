using System.Security.Claims;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Utils;

public static class UserClaimsUtil
{
    public static int Id(this ClaimsPrincipal p)
    {
        return int.Parse(GetValue(p, "Id") ?? "-1");
    }

    public static string Name(this ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Name) ?? "";
    }

    public static string Email(this ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Email) ?? "";
    }

    public static bool IsVerified(this ClaimsPrincipal p)
    {
        return bool.Parse(GetValue(p, "IsVerified") ?? "False");
    }

    public static string Role(this ClaimsPrincipal p)
    {
        return GetValue(p, ClaimTypes.Role) ?? UserRoleType.Normal.ToString();
    }

    private static string? GetValue(this ClaimsPrincipal p, string type)
    {
        return p.Claims.FirstOrDefault(claim => claim.Type == type)?.Value ?? null;
    }
}