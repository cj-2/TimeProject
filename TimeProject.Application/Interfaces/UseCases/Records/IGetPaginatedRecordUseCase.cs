using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetPaginatedRecordUseCase
{
    ICustomResult<IPagination<RecordOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}