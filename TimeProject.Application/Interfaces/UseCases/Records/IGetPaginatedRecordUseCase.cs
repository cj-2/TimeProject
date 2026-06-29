using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetPaginatedRecordUseCase
{
    ICustomResult<IPagination<IRecordOutDto>> Handle(IPaginationQuery paginationQuery, int userId);
}