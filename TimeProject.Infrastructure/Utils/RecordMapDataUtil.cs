using AutoMapper;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.ObjectValues.Records;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Infrastructure.Utils;

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