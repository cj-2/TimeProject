using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IVerifyUserUseCase
{
    Task<ICustomResult<bool>> Handle(int id, string email, string code);
}