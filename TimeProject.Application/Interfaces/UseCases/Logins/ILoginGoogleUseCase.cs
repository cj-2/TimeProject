using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginGoogleUseCase
{
    Task<ICustomResult<JwtResult>> Handle(LoginGoogleDto dto, IUserAccessLog ac);
}