using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.UseCases.Records;

public class GetPaginatedRecordUseCase(IRecordRepository repository, IRecordMapDataUtil mapDataUtil)
    : IGetPaginatedRecordUseCase
{
    public ICustomResult<Pagination<RecordOutDto>> Handle(PaginationQuery paginationQuery, int userId)
    {
        var result = repository.Index(paginationQuery, userId);

        return new CustomResult<Pagination<RecordOutDto>>
        {
            Data = Pagination<RecordOutDto>
                .Handle(mapDataUtil.Handle(result.Entities.ToList()), paginationQuery, result.Count)
        };
    }
}