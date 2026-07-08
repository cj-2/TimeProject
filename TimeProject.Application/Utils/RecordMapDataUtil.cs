using AutoMapper;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

public class RecordMapDataUtil(IMapper mapper) : IRecordMapDataUtil
{
    public List<RecordHistoryDayOutDto> Handle(List<RecordHistoryDayDto> entity)
    {
        return mapper.Map<List<RecordHistoryDayDto>, List<RecordHistoryDayOutDto>>(entity);
    }

    public RecordOutDto Handle(Record entity)
    {
        return mapper.Map<Record, RecordOutDto>(entity);
    }

    public List<RecordOutDto> Handle(List<Record> entities)
    {
        return mapper.Map<List<Record>, List<RecordOutDto>>(entities);
    }
}