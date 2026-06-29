using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Auths;

public class LoginUseCase(
    IJwtHandler jwtHandler,
    IGetUserPasswordByEmailUseCase getUserPasswordByEmailUseCase,
    ICreateUserAccessLogUseCase createUserAccessLogUseCase
)
    : ILoginUseCase
{
    public ICustomResult<JwtResult> Handle(LoginDto dto, IUserAccessLog accessLog)
    {
        var result = new CustomResult<JwtResult>();

        var findUserPasswordResult = getUserPasswordByEmailUseCase.Handle(dto.Email);
        if (findUserPasswordResult.HasError) return result.SetError(findUserPasswordResult.Message);

        var data = findUserPasswordResult.Data!;

        var passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, data.UserPassword.Password);
        if (!passwordMatch) return result.SetError(AuthMessageErrors.WrongEmailOrPassword);

        accessLog.UserId = data.User.UserId;
        createUserAccessLogUseCase.Handle(accessLog);

        return result.SetData(jwtHandler.Generate(data.User));
    }
}