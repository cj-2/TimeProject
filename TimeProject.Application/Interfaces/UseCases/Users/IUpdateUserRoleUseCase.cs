using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IUpdateUserRoleUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto);
}