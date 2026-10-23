using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginGithubUseCase
{
    Task<ICustomResult<JwtDto>> Handle(LoginGithubDto dto, UserAccessLog ac);
}