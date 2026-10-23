using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Utils.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Records.UseCases;

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