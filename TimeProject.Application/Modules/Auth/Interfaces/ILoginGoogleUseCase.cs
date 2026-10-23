using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface ILoginGoogleUseCase
{
    Task<ICustomResult<JwtDto>> Handle(LoginGoogleDto dto, UserAccessLog ac);
}