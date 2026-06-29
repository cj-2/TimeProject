using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto);
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto, IUpdateUserOptions config);
}