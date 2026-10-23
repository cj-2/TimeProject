using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Dtos;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IGetUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id);
}