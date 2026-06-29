using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Users;

public class UpdateRoleDto
{
    [Required] public string Role { get; set; } = string.Empty;
}