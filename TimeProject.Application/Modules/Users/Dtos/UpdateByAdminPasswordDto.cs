using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Users.Dtos;

public class UpdateByAdminPasswordDto
{
    [MinLength(8)]
    [MaxLength(48)]
    [Required]
    public string Password { get; set; } = string.Empty;
}