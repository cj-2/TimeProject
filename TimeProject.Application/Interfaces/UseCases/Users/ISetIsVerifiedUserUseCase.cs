using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ISetIsVerifiedUserUseCase
{
    Task<ICustomResult<bool>> Handle(int id, bool isVerified);
}