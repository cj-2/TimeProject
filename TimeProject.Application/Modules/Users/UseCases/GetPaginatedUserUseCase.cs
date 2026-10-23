using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Users.UseCases;

public class GetPaginatedUserUseCase(
    IUserRepository userRepository,
    IUserAccessLogRepository userAccessLogRepository) : IGetPaginatedUserUseCase
{
    public ICustomResult<Pagination<UserOutDto>> Handle(PaginationQuery paginationQuery)
    {
        var users = userRepository.Index(paginationQuery).Select(UserWithAccessOutDto.Map);
        var totalItems = userRepository.GetTotalItems(paginationQuery);
        var lastAccess = userAccessLogRepository
            .GetLastAccessByUserIdList(users.Select(e => e.UserId).ToList());

        foreach (var user in users)
        {
            var lastUserAccess = lastAccess?.FirstOrDefault(e => e.UserId == user.UserId);
            if (lastUserAccess is null) continue;

            user.SetAccess(lastUserAccess.AccessedAt, lastUserAccess.Type.ToString(),
                lastUserAccess.Provider.ToString() ?? "");
        }

        return new CustomResult<Pagination<UserOutDto>>
            { Data = Pagination<UserOutDto>.Handle(users, paginationQuery, totalItems) };
    }
}