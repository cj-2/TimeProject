using TimeProject.Application.Dtos.Auths;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.UseCases.Auths;

public class LoginUseCase(
    IJwtHandler jwtHandler,
    IUserRepository userRepository,
    IUserPasswordRepository passwordRepository,
    ICreateUserAccessLogUseCase createUserAccessLogUseCase
)
    : ILoginUseCase
{
    public ICustomResult<JwtDto> Handle(LoginDto dto, UserAccessLog accessLog)
    {
        var result = new CustomResult<JwtDto>();
        
        var user = userRepository.FindByEmail(dto.Email);
        if (user == null) return result.SetError(UserErrors.Unauthorized);

        var userPassword = passwordRepository.FindByUserId(user.UserId);
        if (userPassword == null) return result.SetError(UserErrors.PasswordNotAllowed);
        
        var passwordMatch = BCrypt.Net.BCrypt.Verify(dto.Password, userPassword.Password);
        if (!passwordMatch) return result.SetError(AuthErrors.WrongEmailOrPassword);

        accessLog.UserId = userPassword.UserId;
        createUserAccessLogUseCase.Handle(accessLog);

        return result.SetData(jwtHandler.Generate(user));
    }
}