using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordHistoryUseCase
{
    public ICustomResult<IPagination<RecordHistoryDayOutDto>> Handle(int recordId, int userId,
        IPaginationQuery paginationQuery);
}