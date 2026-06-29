using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ICreateUserUseCase
{
    ICustomResult<CreateUserResult> Handle(ICreateUserDto dto);
}