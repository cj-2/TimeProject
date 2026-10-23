using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.UseCases.Users.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Dtos;

public class CreateUserOutDto(JwtDto jwt)
{
    public JwtDto Jwt { get; } = jwt;
}