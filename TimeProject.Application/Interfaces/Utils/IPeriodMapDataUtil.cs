using TimeProject.Application.Dtos.Periods;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface IPeriodMapDataUtil
{
    public PeriodOutDto Handle(Period entity);
    public IEnumerable<PeriodOutDto> Handle(IEnumerable<Period> entity);
}