using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface IUpdateUserRoleUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateRoleDto dto);
}