using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordHistoryUseCase
{
    public ICustomResult<IPagination<RecordHistoryDayOutDto>> Handle(int recordId, int userId,
        IPaginationQuery paginationQuery);
}