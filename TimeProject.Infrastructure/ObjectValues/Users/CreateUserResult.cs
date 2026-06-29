using TimeProject.Domain.Dtos.Auths;
using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Auths;

namespace TimeProject.Infrastructure.ObjectValues.Users;

public class CreateUserResult
{
    public IUserOutDto User { get; set; } = null!;
    public JwtResult Jwt { get; set; } = null!;
}