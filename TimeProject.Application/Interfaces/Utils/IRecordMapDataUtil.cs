using TimeProject.Application.Dtos.Records;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface IRecordMapDataUtil
{
    IEnumerable<RecordHistoryDayOutDto> Handle(IEnumerable<RecordHistoryDayDto> entity);
    RecordOutDto Handle(Record entity);
    IEnumerable<RecordOutDto> Handle(IEnumerable<Record> entities);
}