using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Users.Dtos;

public class UpdateRoleDto
{
    [Required] public string Role { get; set; } = string.Empty;
}