using AutoMapper;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Infrastructure.Database.Entities;

namespace TimeProject.Application.Utils;

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