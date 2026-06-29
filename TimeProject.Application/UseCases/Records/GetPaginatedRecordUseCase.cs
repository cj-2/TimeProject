using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.General;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Records;
using TimeProject.Infrastructure.Utils.Interfaces;

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