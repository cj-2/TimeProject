using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Modules.Users.Dtos;

public class UserWithAccessOutDto : UserOutDto
{
    public DateTime? LastAccess { get; set; }
    public string? LastAccessType { get; set; }
    public string? LastAccessProvider { get; set; }

    public static UserWithAccessOutDto Map(User user)
    {
        return new UserWithAccessOutDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            UserRoleType = user.UserRole,
            IsActive = user.IsActive,
        };
    }

    public void SetAccess(DateTime date, string type, string provider)
    {
        LastAccess = date;
        LastAccessType = type;
        LastAccessProvider = provider;
    }
}