using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.UseCases.Users.Stories.Dtos;

public class UpdateRoleDto
{
    [Required] public string Role { get; set; } = string.Empty;
}