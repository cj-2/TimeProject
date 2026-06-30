using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetPaginatedRecordUseCase
{
    ICustomResult<Pagination<RecordOutDto>> Handle(PaginationQuery paginationQuery, int userId);
}