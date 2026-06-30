using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordHistoryUseCase
{
    public ICustomResult<Pagination<RecordHistoryDayOutDto>> Handle(int recordId, int userId,
        PaginationQuery paginationQuery);
}