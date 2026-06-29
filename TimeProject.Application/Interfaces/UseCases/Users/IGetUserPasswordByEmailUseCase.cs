using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserPasswordByEmailUseCase
{
    ICustomResult<IGetUserPasswordByEmailResult> Handle(string email);
}