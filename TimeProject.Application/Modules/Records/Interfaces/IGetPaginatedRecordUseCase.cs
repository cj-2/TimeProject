using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IGetPaginatedRecordUseCase
{
    ICustomResult<Pagination<RecordOutDto>> Handle(PaginationQuery paginationQuery, int userId);
}