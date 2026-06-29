using AutoMapper;

using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.ObjectValues.Categories;
using TimeProject.Infrastructure.ObjectValues.Codes;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.ObjectValues.Records;
using TimeProject.Infrastructure.ObjectValues.Sessions;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Infrastructure.Mapping;

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