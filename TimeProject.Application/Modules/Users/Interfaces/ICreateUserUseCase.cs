using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Users.Dtos;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateUserUseCase
{
    ICustomResult<CreateUserOutDto> Handle(CreateUserDto dto);
}