
namespace TimeProject.Application.Shared.Interfaces;

public interface ICustomResult
{
    bool IsValid { get; }
    bool HasError { get; set; }
    string? Message { get; set; }
    string? ActionName { get; set; }
    ICustomResult SetError(string? message);
}

public interface ICustomResult<T> : ICustomResult
{
    public T? Data { get; }
    new ICustomResult<T> SetError(string? message);
    ICustomResult<T> SetData(T data);
}