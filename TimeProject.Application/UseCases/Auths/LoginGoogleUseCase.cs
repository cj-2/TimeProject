using RestSharp;
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

public class LoginGoogleUseCase(
    IJwtHandler jwtHandler,
    IGetUserByOAtuhProviderIdUseCase getUserByOAtuhProviderIdUseCase,
    ICreateUserByGoogleUserUseCase createUserByGoogleUserUseCase,
    ICreateUserAccessLogUseCase createUserAccessLogUseCase
)
    : ILoginGoogleUseCase
{
    private readonly RestClient _client = new("https://www.googleapis.com/oauth2/v1/userinfo");

    public async Task<ICustomResult<JwtResult>> Handle(LoginGoogleDto dto, IUserAccessLog ac)
    {
        var result = new CustomResult<JwtResult>();

        try
        {
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {dto.AccessToken}");

            var userFromProvider = (await _client.ExecuteAsync<LoginGoogleResponse>(request)).Data;

            if (userFromProvider is null) return result.SetError(AuthMessageErrors.AuthProviderError);

            var getUserByPIdResult = getUserByOAtuhProviderIdUseCase.Handle("google", userFromProvider.Id);

            if (getUserByPIdResult is { Data: not null })
            {
                ac.UserId = (int)getUserByPIdResult.Data.UserId!;
                createUserAccessLogUseCase.Handle(ac);
                return result.SetData(jwtHandler.Generate(getUserByPIdResult.Data));
            }

            if (userFromProvider.VerifiedEmail == false)
                return result.SetError(UserMessageErrors.UserEmailProviderNotVerified);

            var createUserResult = createUserByGoogleUserUseCase
                .Handle(
                    new CreateUserOAuthDto
                        { Name = userFromProvider.Name, UserProviderId = userFromProvider.Id },
                    userFromProvider.Email
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