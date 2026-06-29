using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.Settings;

namespace TimeProject.Application.Handlers;

public class HookHandler(ICustomBot customBot, TelegramSettings telegramSettings)
    : IHookHandler
{
    public async Task Send(HookTo to, string message)
    {
        var threadId = to switch
        {
            HookTo.Errors => telegramSettings.Threads.Errors,
            HookTo.Users => telegramSettings.Threads.Users,
            HookTo.Feedbacks => telegramSettings.Threads.Feedbacks,
            _ => null
        };

        var now = DateTime.Now.ToUniversalTime();
        message += $"\n\n{TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.Local):dd/MM/yyyy HH:mm:ss}";
        message += $"\nUTC: {now}";
        
        await customBot.SendMessage(telegramSettings.ChatId, message, threadId);
    }

    public async Task SendError(string message)
    {
        await Send(HookTo.Errors, message);
    }
}