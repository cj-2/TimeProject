using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Application.UseCases.Users.Queries.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.UseCases.Users.Queries;

public class GetUserUseCase(
    IUserRepository userRepository,
    IUserAccessLogRepository userAccessLogRepository,
    IUserMapDataUtil mapper
) : IGetUserUseCase
{
    public ICustomResult<UserOutDto> Handle(int id)
    {
        var result = new CustomResult<UserOutDto>();
        var user = userRepository.FindById(id);

        if (user == null)
            return result.SetError(UserMessageErrors.NotFound);

        var userMapped = mapper.Handle(user);

        var lastAccess = userAccessLogRepository.GetLastAccessByUserId(id);
        if (lastAccess is null) return result.SetData(userMapped);

        userMapped.LastAccess = lastAccess.AccessedAt;
        userMapped.LastAccessType = lastAccess.Type.ToString();
        userMapped.LastAccessProvider = lastAccess.Provider.ToString();

        return result.SetData(userMapped);
    }
}