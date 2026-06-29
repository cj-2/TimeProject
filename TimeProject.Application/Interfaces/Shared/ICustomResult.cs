
namespace TimeProject.Application.Interfaces.Shared;

public interface ICustomResult<T>
{
    public T? Data { get; }
    bool IsValid { get; }
    bool HasError { get; set; }
    string? Message { get; set; }
    string? ActionName { get; set; }
    ICustomResult<T> SetError(string? message);
    ICustomResult<T> SetData(T data);
}