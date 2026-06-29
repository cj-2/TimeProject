using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Infrastructure.Utils.Interfaces;

public interface IRecordMapDataUtil
{
    IEnumerable<RecordHistoryDayOutDto> Handle(IEnumerable<RecordHistoryDayDto> entity);

    RecordOutDto Handle(IRecord entity);

    IEnumerable<RecordOutDto> Handle(IEnumerable<IRecord> entities);
}