using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Users;

public class RecoveryDto
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
}