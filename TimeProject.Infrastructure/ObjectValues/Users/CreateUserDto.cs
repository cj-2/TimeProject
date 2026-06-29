using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Users;

public class CreateUserDto
{
    [MinLength(2)] [MaxLength(120)] public string Name { get; set; } = string.Empty;
    [EmailAddress] [MaxLength(64)] public string Email { get; set; } = string.Empty;
    [MinLength(8)] [MaxLength(48)] public string Password { get; set; } = string.Empty;
    [Required] [MaxLength(64)] public string Timezone { get; set; } = "";
}