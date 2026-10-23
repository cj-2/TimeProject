using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Application.UseCases.Users.Dtos;

namespace TimeProject.Application.Modules.Users.Dtos;

public class CreateUserOutDto(JwtDto jwt)
{
    public JwtDto Jwt { get; } = jwt;
}