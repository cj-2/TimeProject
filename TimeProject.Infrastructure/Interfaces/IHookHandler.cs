namespace TimeProject.Infrastructure.Interfaces;

public enum HookTo
{
    General,
    Errors,
    Users,
    Feedbacks
}

public interface IHookHandler
{
    public Task Send(HookTo to, string message);
    public Task SendError(string message);
}