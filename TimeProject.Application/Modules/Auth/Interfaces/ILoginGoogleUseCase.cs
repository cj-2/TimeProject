using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginGoogleUseCase
{
    Task<ICustomResult<JwtDto>> Handle(LoginGoogleDto dto, UserAccessLog ac);
}