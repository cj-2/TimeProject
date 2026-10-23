using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils.Interfaces;

public interface IRecordMapDataUtil
{
    List<RecordHistoryDayOutDto> Handle(List<RecordHistoryDayDto> entity);
    RecordOutDto Handle(Record entity);
    List<RecordOutDto> Handle(List<Record> entities);
}