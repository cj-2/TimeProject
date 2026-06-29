using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginUseCase
{
    ICustomResult<JwtDto> Handle(LoginDto dto, IUserAccessLog accessLog);
}