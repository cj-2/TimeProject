using System.ComponentModel.DataAnnotations;

namespace TimeProject.Infrastructure.ObjectValues.Auths;

public class LoginGoogleDto
{
    [Required] public string AccessToken { get; set; } = string.Empty;
}