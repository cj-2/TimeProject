using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetPaginatedUserUseCase
{
    ICustomResult<IPagination<UserOutDto>> Handle(IPaginationQuery paginationQuery);
}