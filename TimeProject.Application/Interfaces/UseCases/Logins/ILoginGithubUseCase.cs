using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Auths;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginGithubUseCase
{
    Task<ICustomResult<JwtResult>> Handle(LoginGithubDto dto, IUserAccessLog ac);
}