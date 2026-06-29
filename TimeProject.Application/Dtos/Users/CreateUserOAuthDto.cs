using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Users;

public class CreateUserOAuthDto
{
    [MinLength(2)] [MaxLength(120)] public string Name { get; set; } = string.Empty;
    [Required] public string UserProviderId { get; set; } = string.Empty;
    [Required] [Range(-12, 13)] public int Utc { get; set; } = -3;
}