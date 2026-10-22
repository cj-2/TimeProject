using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Shared;

public class CustomResult : ICustomResult
{
    public bool IsValid => !HasError;
    public bool HasError { get; set; }
    public string? Message { get; set; }
    public string? ActionName { get; set; }

    public ICustomResult SetError(string? message)
    {
        HasError = true;
        Message = message;
        return this;
    }
}

public class CustomResult<T> : CustomResult, ICustomResult<T>
{
    public T? Data { get; set; }

    public new ICustomResult<T> SetError(string? message)
    {
        HasError = true;
        Message = message;
        return this;
    }

    public ICustomResult<T> SetData(T data)
    {
        Data = data;
        return this;
    }
}