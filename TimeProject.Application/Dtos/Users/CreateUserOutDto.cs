using TimeProject.Application.Dtos.Auths;

namespace TimeProject.Application.Dtos.Users;

public class CreateUserOutDto
{
    public UserOutDto User { get; set; } = null!;
    public JwtResult Jwt { get; set; } = null!;
}