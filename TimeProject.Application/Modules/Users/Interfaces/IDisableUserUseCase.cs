using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IDisableUserUseCase
{
    ICustomResult<bool> Handle(int id, DisableUserDto dto);
}