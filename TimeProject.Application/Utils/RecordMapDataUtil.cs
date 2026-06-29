using AutoMapper;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class RecordMapDataUtil(IMapper mapper) : IRecordMapDataUtil
{
    public IEnumerable<RecordHistoryDayOutDto> Handle(IEnumerable<RecordHistoryDayDto> entity)
    {
        return mapper.Map<IEnumerable<RecordHistoryDayDto>, IEnumerable<RecordHistoryDayOutDto>>(entity);
    }

    public RecordOutDto Handle(IRecord entity)
    {
        return mapper.Map<IRecord, RecordOutDto>(entity);
    }

    public IEnumerable<RecordOutDto> Handle(IEnumerable<IRecord> entities)
    {
        return mapper.Map<IEnumerable<IRecord>, IEnumerable<RecordOutDto>>(entities);
    }
}