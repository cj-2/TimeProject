namespace TimeProject.Application.Dtos.Auths;

public class JwtDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}