using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.UseCases.Users.Stories.Dtos;

public class CreatePasswordDto
{
    [MinLength(8)]
    [MaxLength(48)]
    [Required]
    public string Password { get; set; } = string.Empty;
}