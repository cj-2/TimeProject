using Octokit;
using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Auths;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Auths;

public class LoginGithubUseCase(
    IJwtHandler jwtHandler,
    IGetUserByOAtuhProviderIdUseCase getUserByOAtuhProviderIdUseCase,
    ICreateUserByGhUserUseCase createUserByGhUserUseCase,
    ICreateUserAccessLogUseCase createUserAccessLogUseCase
)
    : ILoginGithubUseCase
{
    public async Task<ICustomResult<JwtResult>> Handle(LoginGithubDto dto, IUserAccessLog ac)
    {
        var result = new CustomResult<JwtResult>();

        try
        {
            var client = new GitHubClient(new ProductHeaderValue("RMTA"))
            {
                Credentials = new Credentials(dto.AccessToken, AuthenticationType.Bearer)
            };

            var userFromProvider = await client.User.Current();
            var getUserByPIdResult =
                getUserByOAtuhProviderIdUseCase.Handle("github", userFromProvider.Id.ToString());

            if (getUserByPIdResult is { Data: not null })
            {
                ac.UserId = (int)getUserByPIdResult.Data.UserId!;
                createUserAccessLogUseCase.Handle(ac);
                return result.SetData(jwtHandler.Generate(getUserByPIdResult.Data));
            }

            var emailList = await client.User.Email.GetAll();

            var createUserResult = createUserByGhUserUseCase
                .Handle(
                    new CreateUserOAuthDto
                        { Name = userFromProvider.Name, UserProviderId = userFromProvider.Id.ToString() },
                    emailList.Select(e => new EmailGh(e.Email, e.Primary, e.Verified))
                );


            var createIsSuccess = createUserResult is { Data: not null };

            if (!createIsSuccess) result.SetError(createUserResult.Message);

            ac.UserId = (int)createUserResult.Data!.UserId!;
            createUserAccessLogUseCase.Handle(ac);

            return result.SetData(jwtHandler.Generate(createUserResult.Data));
        }
        catch
        {
            return result.SetError(AuthMessageErrors.AuthProviderError);
        }
    }
}