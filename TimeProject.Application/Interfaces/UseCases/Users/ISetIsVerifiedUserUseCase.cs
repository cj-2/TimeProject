using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ISetIsVerifiedUserUseCase
{
    Task<ICustomResult<bool>> Handle(int id, bool isVerified);
}