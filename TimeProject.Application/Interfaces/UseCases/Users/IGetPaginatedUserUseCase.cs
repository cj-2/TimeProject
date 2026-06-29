using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetPaginatedUserUseCase
{
    ICustomResult<IPagination<UserOutDto>> Handle(IPaginationQuery paginationQuery);
}