using AutoMapper;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Infrastructure.Utils;

public class PeriodMapDataUtil(IMapper mapper) : IPeriodMapDataUtil
{
    public PeriodOutDto Handle(Period entity)
    {
        return mapper.Map<Period, PeriodOutDto>(entity);
    }

    public IEnumerable<PeriodOutDto> Handle(IEnumerable<Period> entity)
    {
        return mapper.Map<IEnumerable<Period>, IEnumerable<PeriodOutDto>>(entity);
    }
}