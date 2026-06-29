using AutoMapper;
using TimeProject.Application.Dtos.Categories;
using TimeProject.Application.Dtos.Codes;
using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Dtos.Sessions;
using TimeProject.Application.Dtos.Users;
using TimeProject.Infrastructure.Database.Entities;

namespace TimeProject.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserOutDto>();
        CreateMap<Record, RecordOutDto>();
        CreateMap<Period, PeriodOutDto>();
        CreateMap<Category, CategoryOutDto>();
        // CreateMap<Category, CategoryOutDto>()
        //     .ConstructUsing(src => new CategoryOutDto());
        CreateMap<RecordHistoryDayDto, RecordHistoryDayOutDto>();
        CreateMap<Session, SessionOutDto>();
        CreateMap<ConfirmCode, ConfirmCodeOutDto>();
        CreateMap<RecordResume, RecordResumeOutDto>();
        // CreateMap<RecordResume, RecordResumeOutDto>()
        //     .ConstructUsing(src => new RecordResumeOutDto());
    }
}