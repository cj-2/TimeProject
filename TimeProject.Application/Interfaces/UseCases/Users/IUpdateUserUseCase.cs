using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IUpdateUserUseCase
{
    ICustomResult<IUserOutDto> Handle(int id, IUpdateUserDto dto);
    ICustomResult<IUserOutDto> Handle(int id, IUpdateUserDto dto, IUpdateUserOptions config);
}