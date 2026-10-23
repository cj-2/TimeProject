using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IGetUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id);
}