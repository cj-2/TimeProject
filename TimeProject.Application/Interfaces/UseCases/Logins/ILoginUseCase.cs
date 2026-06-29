using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Auths;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginUseCase
{
    ICustomResult<JwtResult> Handle(LoginDto dto, IUserAccessLog accessLog);
}