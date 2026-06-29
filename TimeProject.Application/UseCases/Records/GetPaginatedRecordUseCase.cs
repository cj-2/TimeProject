using TimeProject.Application.Dtos.General;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Records;

public class GetPaginatedRecordUseCase(IRecordRepository repository, IRecordMapDataUtil mapDataUtil)
    : IGetPaginatedRecordUseCase
{
    public ICustomResult<IPagination<RecordOutDto>> Handle(IPaginationQuery paginationQuery, int userId)
    {
        var result = repository.Index(paginationQuery, userId);

        return new CustomResult<IPagination<RecordOutDto>>
        {
            Data = Pagination<RecordOutDto>
                .Handle(mapDataUtil.Handle(result.Entities), paginationQuery, result.Count)
        };
    }
}