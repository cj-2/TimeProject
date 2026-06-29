using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetPaginatedRecordUseCase
{
    ICustomResult<IPagination<RecordOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}