using AutoMapper;
using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Application.Dtos.Codes;
using TimeProject.Application.Dtos.Minutes;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Dtos.Sessions;
using TimeProject.Application.UseCases.Users.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserOutDto>();
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