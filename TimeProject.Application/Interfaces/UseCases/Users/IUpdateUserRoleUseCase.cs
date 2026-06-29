using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IUpdateUserRoleUseCase
{
    ICustomResult<IUserOutDto> Handle(int id, IUpdateRoleDto dto);
}