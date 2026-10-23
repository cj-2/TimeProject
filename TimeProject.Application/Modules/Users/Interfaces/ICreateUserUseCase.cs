using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateUserUseCase
{
    ICustomResult<CreateUserOutDto> Handle(CreateUserDto dto);
}