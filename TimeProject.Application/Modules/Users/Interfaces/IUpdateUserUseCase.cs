using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Options;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IUpdateUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto);
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto, UpdateUserOptions options);
}