using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Users.UseCases;

public class GetUserByOAuthProviderIdUseCase(IUserRepository repository, IUserProviderRepository oAuthRepository)
    : IGetUserByOAtuhProviderIdUseCase
{
    public ICustomResult<User> Handle(string provider, string id)
    {
        var result = new CustomResult<User>();

        var userOAuth = oAuthRepository.FindByUserProviderId(provider, id);
        if (userOAuth == null) return result.SetError(UserErrors.NotFound);

        var user = repository.FindById(userOAuth.UserId);
        return user == null ? result.SetError(UserErrors.NotFound) : result.SetData(user);
    }
}