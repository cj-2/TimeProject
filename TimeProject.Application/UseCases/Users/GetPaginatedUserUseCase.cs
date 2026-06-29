using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class GetPaginatedUserUseCase(
    IUserRepository userRepository,
    IUserAccessLogRepository userAccessLogRepository,
    IUserMapDataUtil mapper) : IGetPaginatedUserUseCase
{
    public ICustomResult<IPagination<UserOutDto>> Handle(IPaginationQuery paginationQuery)
    {
        var data = mapper.Handle(userRepository.Index(paginationQuery));
        var totalItems = userRepository.GetTotalItems(paginationQuery);
        var lastAccess = userAccessLogRepository
            .GetLastAccessByUserIdList(data.Select(e => e.UserId).ToList());

        foreach (var user in data)
        {
            var lastUserAccess = lastAccess?.FirstOrDefault(e => e.UserId == user.UserId);
            if (lastUserAccess is null) continue;

            user.LastAccess = lastUserAccess.AccessedAt;
            user.LastAccessType = lastUserAccess.Type.ToString();
            user.LastAccessProvider = lastUserAccess.Provider.ToString();
        }

        return new CustomResult<IPagination<UserOutDto>>
            { Data = Pagination<UserOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}