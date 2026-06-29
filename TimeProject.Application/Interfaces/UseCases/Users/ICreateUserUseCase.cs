using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateUserUseCase
{
    ICustomResult<CreateUserOutDto> Handle(CreateUserDto dto);
}