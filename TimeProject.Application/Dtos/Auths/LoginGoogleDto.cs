using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Auths;

public class LoginGoogleDto
{
    [Required] public string AccessToken { get; set; } = string.Empty;
}