using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetPaginatedUserUseCase
{
    ICustomResult<Pagination<UserOutDto>> Handle(PaginationQuery paginationQuery);
}