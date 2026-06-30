using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginGithubUseCase
{
    Task<ICustomResult<JwtDto>> Handle(LoginGithubDto dto, UserAccessLog ac);
}