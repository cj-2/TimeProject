using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Users;

public class GetUserByOAuthProviderIdUseCase(IUserRepository repository, IUserProviderRepository oAuthRepository)
    : IGetUserByOAtuhProviderIdUseCase
{
    public ICustomResult<IUser> Handle(string provider, string id)
    {
        var result = new CustomResult<IUser>();

        var userOAuth = oAuthRepository.FindByUserProviderId(provider, id);
        if (userOAuth == null) return result.SetError(UserMessageErrors.NotFound);

        var user = repository.FindById(userOAuth.UserId);
        return user == null ? result.SetError(UserMessageErrors.NotFound) : result.SetData(user);
    }
}