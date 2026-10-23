using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Users.UseCases;

public class GetUserUseCase(
    IUserRepository userRepository,
    IUserAccessLogRepository userAccessLogRepository
) : IGetUserUseCase
{
    public ICustomResult<UserOutDto> Handle(int id)
    {
        var result = new CustomResult<UserOutDto>();
        var user = userRepository.FindById(id);

        if (user == null)
            return result.SetError(UserErrors.NotFound);

        var userMapped = UserWithAccessOutDto.Map(user);

        var lastAccess = userAccessLogRepository.GetLastAccessByUserId(id);
        if (lastAccess is null) return result.SetData(userMapped);

        userMapped.SetAccess(lastAccess.AccessedAt, lastAccess.Type.ToString(), lastAccess.Provider.ToString() ?? "");

        return result.SetData(userMapped);
    }
}