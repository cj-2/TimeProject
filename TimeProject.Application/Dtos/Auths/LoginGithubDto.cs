using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Auths;

public class LoginGithubDto
{
    [Required] public string AccessToken { get; set; } = string.Empty;
    [Required] public string TokenType { get; set; } = string.Empty;
}