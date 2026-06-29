using Microsoft.Extensions.Options;
using TimeProject.APIs.Controllers.Middlewares;
using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Handlers;
using TimeProject.Application.Interfaces.Handlers;
using TimeProject.Application.Interfaces.UseCases.Categories;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Application.Interfaces.UseCases.Logins;
using TimeProject.Application.Interfaces.UseCases.Minutes;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.UseCases.Sessions;
using TimeProject.Application.Interfaces.UseCases.Statistics;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Mapping;
using TimeProject.Application.UseCases.Auths;
using TimeProject.Application.UseCases.Categories;
using TimeProject.Application.UseCases.Codes;
using TimeProject.Application.UseCases.CustomLogs;
using TimeProject.Application.UseCases.Feedbacks;
using TimeProject.Application.UseCases.Statistics;
using TimeProject.Application.UseCases.Minutes;
using TimeProject.Application.UseCases.Periods;
using TimeProject.Application.UseCases.Records;
using TimeProject.Application.UseCases.Sessions;
using TimeProject.Application.UseCases.Users;
using TimeProject.Application.Utils;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Integrations;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.Repositories;
using TimeProject.Infrastructure.Settings;

namespace TimeProject.APIs.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        #region Settings

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));

        services.Configure<TelegramSettings>(configuration.GetSection("Telegram"));
        services.Configure<TurnstileSettings>(configuration.GetSection("Turnstile"));

        services.AddSingleton(provider => provider.GetRequiredService<IOptions<JwtSettings>>().Value);
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<SmtpSettings>>().Value);

        services.AddSingleton(provider => provider.GetRequiredService<IOptions<TelegramSettings>>().Value);
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<TurnstileSettings>>().Value);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        #endregion

        #region Maps

        services.AddAutoMapper(typeof(MappingProfile));

        #endregion

        #region Integrations

        services.AddScoped<ICustomSmtp, CustomSmtp>();
        services.AddSingleton<ICustomBot, CustomBot>();
        services.AddSingleton<IUserChallenge, UserChallenge>();

        #endregion

        #region Handlers

        services.AddScoped<IEmailHandler, EmailHandler>();
        services.AddScoped<IHookHandler, HookHandler>();

        #endregion

        #region Jwt

        services.AddSingleton<IJwtHandler, JwtHandler>();

        #endregion

        #region User

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();

        services.AddScoped<IUserMapDataUtil, UserMapDataUtil>();
        services.AddScoped<IUpdateUserOptions, UpdateUserOptions>();

        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<ICreateUserByGhUserUseCase, CreateUserByGhUserUseCase>();
        services.AddScoped<ICreateUserByGoogleUserUseCase, CreateUserByGoogleUserUseCase>();
        services.AddScoped<IUpdateUserRoleUseCase, UpdateUserRoleUseCase>();
        services
            .AddScoped<ICreateOrUpdateUserPasswordByEmailUseCase, CreateOrUpdateUserPasswordByEmailUseCase>();
        services.AddScoped<IDisableUserUseCase, DisableUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IGetUserByEmailUseCase, GetUserByEmailUseCase>();
        services.AddScoped<IGetUserPasswordByEmailUseCase, GetUserPasswordByEmailUseCase>();
        services.AddScoped<IGetUserByOAtuhProviderIdUseCase, GetUserByOAuthProviderIdUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IGetPaginatedUserUseCase, GetPaginatedUserUseCase>();
        // services.AddScoped<ISetIsVerifiedUserUseCase, SetIsVerifiedUserUseCase>();

        services.AddScoped<ICreateOrUpdateUserPasswordUseCase, CreateOrUpdateUserPasswordUseCase>();

        #endregion

        #region Auth

        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ISendRecoveryEmailUseCase, SendRecoveryEmailUseCase>();
        services.AddScoped<IRecoveryPasswordUseCase, RecoveryPasswordUseCase>();
        services.AddScoped<ISendRegisterEmailUseCase, SendRegisterEmailUseCase>();
        // services.AddScoped<IVerifyUserUseCase, VerifyUserUseCase>();

        #endregion

        #region OAuth

        services.AddScoped<IUserProviderRepository, UserProviderRepository>();
        services.AddScoped<ILoginGithubUseCase, LoginGithubUseCase>();
        services.AddScoped<ILoginGoogleUseCase, LoginGoogleUseCase>();

        #endregion

        #region Records

        services.AddScoped<IRecordRepository, RecordRepository>();
        services.AddScoped<IRecordResumeRepository, RecordResumeRepository>();
        services.AddScoped<IRecordHistoryRepository, RecordHistoryRepository>();

        services.AddScoped<IRecordMapDataUtil, RecordMapDataUtil>();

        services.AddScoped<IGetRecordHistoryUseCase, GetRecordHistoryUseCase>();

        services.AddScoped<IGetPaginatedRecordUseCase, GetPaginatedRecordUseCase>();
        services.AddScoped<IGetRecordByCodeUseCase, GetRecordByCodeUseCase>();
        services.AddScoped<IGetRecordByIdUseCase, GetRecordByIdUseCase>();
        services.AddScoped<ICreateRecordUseCase, CreateRecordUseCase>();
        services.AddScoped<IUpdateRecordUseCase, UpdateRecordUseCase>();
        services.AddScoped<IDeleteRecordUseCase, DeleteRecordUseCase>();
        services.AddScoped<ISearchRecordUseCase, SearchRecordUseCase>();

        services.AddScoped<ISyncRecordResumeUseCase, SyncRecordResumeUseCase>();
        services.AddScoped<ISyncAllRecordResumeUseCase, SyncAllRecordResumeUseCase>();

        #endregion

        #region TimerSession

        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IDeleteSessionUseCase, DeleteSessionUseCase>();

        #endregion

        #region Period

        services.AddScoped<IPeriodRepository, PeriodRepository>();

        services.AddScoped<IPeriodMapDataUtil, PeriodMapDataUtil>();
        services.AddScoped<IPeriodCutUtil, PeriodCutUtil>();
        services.AddScoped<IPeriodValidateUtil, PeriodValidateUtil>();

        services.AddScoped<IGetPaginatedPeriodUseCase, GetPaginatedPeriodUseCase>();
        services.AddScoped<ICreatePeriodByListUseCase, CreatePeriodByListUseCase>();
        services.AddScoped<ICreatePeriodUseCase, CreatePeriodUseCase>();
        services.AddScoped<IUpdatePeriodUseCase, UpdatePeriodUseCase>();
        services.AddScoped<IDeletePeriodUseCase, DeletePeriodUseCase>();

        #endregion

        #region Category

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryMapDataUtil, CategoryMapDataUtil>();

        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
        services.AddScoped<IGetAllCategoryUseCase, GetAllCategoryUseCase>();
        services.AddScoped<IGetPaginatedCategoryUseCase, GetPaginatedCategoryUseCase>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

        #endregion

        #region Code

        services.AddScoped<IConfirmCodeRepository, ConfirmCodeRepository>();

        services.AddScoped<ICreateConfirmCodeUseCase, CreateConfirmCodeUseCase>();
        services.AddScoped<IValidateConfirmCodeUseCase, ValidateConfirmCodeUseCase>();
        services.AddScoped<ISetIsUsedConfirmCodeUseCase, SetIsUsedConfirmCodeUseCase>();
        services.AddScoped<ISetWasSentConfirmCodeUseCase, SetWasSentConfirmCodeUseCase>();
        services.AddScoped<IGetRegisterCodeInfoUseCase, GetRegisterCodeInfoUseCase>();

        #endregion

        #region Statistics

        services.AddScoped<IStatisticRepository, StatisticRepository>();
        services.AddScoped<IGetRangeDaysStatisticUseCase, GetRangeDaysStatisticUseCase>();

        #endregion

        #region Feedback

        services.AddScoped<ISendFeedbackUseCase, SendFeedbackUseCase>();
        services.AddScoped<ISendPublicFeedbackUseCase, SendPublicFeedbackUseCase>();

        #endregion

        #region TimeMinute

        services.AddScoped<IMinuteRepository, MinuteRepository>();
        services.AddScoped<ICreateMinuteByListUseCase, CreateMinuteByListUseCase>();
        services.AddScoped<IDeleteMinuteUseCase, DeleteMinuteUseCase>();

        #endregion

        #region Logs

        services.AddScoped<IUserAccessLogRepository, UserAccessLogRepository>();
        services.AddScoped<ICreateUserAccessLogUseCase, CreateUserAccessLogUseCase>();

        #endregion

        #region Middlewares

        services.AddSingleton<UserChallengeMiddleware>();

        #endregion

        return services;
    }
}