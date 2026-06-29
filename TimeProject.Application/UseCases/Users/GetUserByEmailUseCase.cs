using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Users;

public class GetUserByEmailUseCase(IUserRepository repository) : IGetUserByEmailUseCase
{
    public ICustomResult<IUser> Handle(string email)
    {
        var result = new CustomResult<IUser>();
        var user = repository.FindByEmail(email);

        return user == null ? result.SetError(UserMessageErrors.NotFound) : result.SetData(user);
    }
}