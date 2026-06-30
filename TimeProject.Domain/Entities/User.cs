using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRoleType UserRole { get; set; } = UserRoleType.Normal;
    public string Timezone { get; set; }
    public bool IsActive { get; set; } = true;
}