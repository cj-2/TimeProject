using TimeProject.Infrastructure.ObjectValues.Auths;

namespace TimeProject.Infrastructure.ObjectValues.Users;

public class CreateUserOutDto
{
    public UserOutDto User { get; set; } = null!;
    public JwtResult Jwt { get; set; } = null!;
}