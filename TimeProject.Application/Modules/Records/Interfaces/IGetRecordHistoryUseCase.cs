using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IGetRecordHistoryUseCase
{
    public ICustomResult<Pagination<RecordHistoryDayOutDto>> Handle(int recordId, int userId,
        PaginationQuery paginationQuery);
}