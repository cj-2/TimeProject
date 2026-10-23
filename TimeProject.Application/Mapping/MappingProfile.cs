using AutoMapper;
using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Modules.Codes.Dtos;
using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Modules.Minutes.Dtos;
using TimeProject.Application.Modules.Sessions.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Record, RecordOutDto>();
        CreateMap<Period, PeriodOutDto>();
        CreateMap<Category, CategoryOutDto>();
        CreateMap<RecordHistoryDayDto, RecordHistoryDayOutDto>();
        CreateMap<Session, SessionOutDto>();
        CreateMap<Minute, MinuteOutDto>();
        CreateMap<ConfirmCode, ConfirmCodeOutDto>();
        CreateMap<RecordResume, RecordResumeOutDto>();
    }
}