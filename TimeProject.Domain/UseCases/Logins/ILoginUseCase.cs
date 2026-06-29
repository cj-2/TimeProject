using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Dtos.Auths;
using TimeProject.Domain.Shared;

namespace TimeProject.Domain.UseCases.Logins;

public interface ILoginUseCase
{
    ICustomResult<IJwtResult> Handle(ILoginDto dto, IUserAccessLog accessLog);
}