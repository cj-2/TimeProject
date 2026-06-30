using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Users;

public class GetUserPasswordByEmailUseCase(IUserPasswordRepository repository, IUserRepository userRepository)
    : IGetUserPasswordByEmailUseCase
{
    public ICustomResult<GetUserPasswordByEmailOutDto> Handle(string email)
    {
        var result = new CustomResult<GetUserPasswordByEmailOutDto>();

        var user = userRepository.FindByEmail(email);
        if (user == null) return result.SetError(UserMessageErrors.NotFound);

        var userPassword = repository.FindByUserId(user.UserId);
        return userPassword == null
            ? result.SetError(UserMessageErrors.PasswordNotAllowed)
            : result.SetData(new GetUserPasswordByEmailOutDto
                { UserPassword = userPassword, User = user });
    }
}