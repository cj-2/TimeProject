using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IGetPaginatedUserUseCase
{
    ICustomResult<Pagination<UserOutDto>> Handle(PaginationQuery paginationQuery);
}