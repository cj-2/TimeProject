using TimeProject.Domain.Entities.Enums;
using TimeProject.Infrastructure.Database.Entities.Enums;

namespace TimeProject.Infrastructure.ObjectValues.Users;

public class UserOutDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRoleType UserRoleType { get; set; }
    public string UserRoleLabel => UserRoleType.ToString();

    public bool IsActive { get; set; }
    public bool IsAdmin => UserRoleType == UserRoleType.Admin;

    public DateTime? LastAccess { get; set; }
    public string? LastAccessType { get; set; }
    public string? LastAccessProvider { get; set; }
}