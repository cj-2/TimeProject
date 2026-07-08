using TimeProject.Application.Dtos.Records;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface IRecordMapDataUtil
{
    List<RecordHistoryDayOutDto> Handle(List<RecordHistoryDayDto> entity);
    RecordOutDto Handle(Record entity);
    List<RecordOutDto> Handle(List<Record> entities);
}