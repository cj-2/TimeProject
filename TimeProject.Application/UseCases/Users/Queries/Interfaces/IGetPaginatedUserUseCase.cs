using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.UseCases.Users.Queries.Interfaces;

public interface IGetPaginatedUserUseCase
{
    ICustomResult<Pagination<UserOutDto>> Handle(PaginationQuery paginationQuery);
}