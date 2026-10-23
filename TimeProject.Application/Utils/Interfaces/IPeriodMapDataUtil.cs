using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils.Interfaces;

public interface IPeriodMapDataUtil
{
    public PeriodOutDto Handle(Period entity);
    public IEnumerable<PeriodOutDto> Handle(IEnumerable<Period> entity);
}