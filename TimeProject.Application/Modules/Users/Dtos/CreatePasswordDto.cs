using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Users.Dtos;

public class CreatePasswordDto
{
    [MinLength(8)]
    [MaxLength(48)]
    [Required]
    public string Password { get; set; } = string.Empty;
}