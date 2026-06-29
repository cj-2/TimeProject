using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Users;

public class UpdatePasswordDto
{
    [MinLength(8)]
    [MaxLength(48)]
    [Required]
    public string Password { get; set; } = string.Empty;

    [MinLength(8)]
    [MaxLength(48)]
    [Required]
    public string OldPassword { get; set; } = string.Empty;
}