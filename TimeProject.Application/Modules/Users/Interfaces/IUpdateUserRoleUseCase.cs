using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IUpdateUserRoleUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto);
}