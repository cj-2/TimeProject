using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Users;

public class UpdateRoleDto
{
    [Required] public string Role { get; set; } = string.Empty;
}