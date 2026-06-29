using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Users;

public class UpdateUserDto
{
    [MinLength(2)] [MaxLength(120)] public string? Name { get; set; }
    [EmailAddress] [MaxLength(64)] public string? Email { get; set; }
    [MinLength(8)] [MaxLength(48)] public string? Password { get; set; }
    [Range(-12, 13)] public string? Timezone { get; set; }
}