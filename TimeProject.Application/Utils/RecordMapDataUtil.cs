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

    public RecordOutDto Handle(Record entity)
    {
        return mapper.Map<Record, RecordOutDto>(entity);
    }

    public IEnumerable<RecordOutDto> Handle(IEnumerable<Record> entities)
    {
        return mapper.Map<IEnumerable<Record>, IEnumerable<RecordOutDto>>(entities);
    }
}