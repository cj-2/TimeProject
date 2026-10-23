using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginUseCase
{
    ICustomResult<JwtDto> Handle(LoginDto dto, UserAccessLog accessLog);
}