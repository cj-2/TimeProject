using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.UseCases.Users.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Dtos;

public class CreateUserOutDto
{
    public UserOutDto User { get; set; } = null!;
    public JwtDto Jwt { get; set; } = null!;
}