using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Auth.Dtos;

public class LoginGoogleDto
{
    [Required] public string AccessToken { get; set; } = string.Empty;
}