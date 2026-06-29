using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Auths;

namespace TimeProject.Application.Interfaces.UseCases.Logins;

public interface ILoginGoogleUseCase
{
    Task<ICustomResult<JwtResult>> Handle(LoginGoogleDto dto, IUserAccessLog ac);
}