using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginGithubUseCase
{
    Task<ICustomResult<JwtDto>> Handle(LoginGithubDto dto, UserAccessLog ac);
}