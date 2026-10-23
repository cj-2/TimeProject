using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.UseCases.Users.Dtos;

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

    public static UserOutDto Map(User user)
    {
        return new UserOutDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            UserRoleType = user.UserRole,
            IsActive = user.IsActive
        };
    }
}