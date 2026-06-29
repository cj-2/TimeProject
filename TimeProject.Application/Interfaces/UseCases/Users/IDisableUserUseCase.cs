using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IDisableUserUseCase
{
    ICustomResult<bool> Handle(int id, DisableUserDto dto);
}