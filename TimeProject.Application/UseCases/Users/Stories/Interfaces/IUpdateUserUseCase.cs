using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Options;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface IUpdateUserUseCase
{
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto);
    ICustomResult<UserOutDto> Handle(int id, UpdateUserDto dto, UpdateUserOptions options);
}