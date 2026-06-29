namespace TimeProject.Infrastructure.ObjectValues.Auths;

public class JwtResult
{
    public string Token { get; set; } = string.Empty;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}