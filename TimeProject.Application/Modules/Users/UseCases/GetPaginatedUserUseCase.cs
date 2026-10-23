using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Users.UseCases;

public class GetPaginatedUserUseCase(
    IUserRepository userRepository,
    IUserAccessLogRepository userAccessLogRepository,
    IUserMapDataUtil mapper) : IGetPaginatedUserUseCase
{
    public ICustomResult<Pagination<UserOutDto>> Handle(PaginationQuery paginationQuery)
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

        return new CustomResult<Pagination<UserOutDto>>
            { Data = Pagination<UserOutDto>.Handle(data, paginationQuery, totalItems) };
    }
}