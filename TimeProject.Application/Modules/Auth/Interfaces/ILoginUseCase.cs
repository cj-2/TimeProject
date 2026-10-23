using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginUseCase
{
    ICustomResult<JwtDto> Handle(LoginDto dto, UserAccessLog accessLog);
}