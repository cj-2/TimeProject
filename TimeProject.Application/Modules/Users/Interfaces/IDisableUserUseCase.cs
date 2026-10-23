using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Users.Dtos;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IDisableUserUseCase
{
    ICustomResult<bool> Handle(int id, DisableUserDto dto);
}