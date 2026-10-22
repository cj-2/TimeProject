using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface IDisableUserUseCase
{
    ICustomResult<bool> Handle(int id, DisableUserDto dto);
}