using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordHistoryUseCase
{
    public ICustomResult<IPagination<IRecordHistoryDayOutDto>> Handle(int recordId, int userId,
        IPaginationQuery paginationQuery);
}