using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Minutes.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Minutes;

public class DeleteMinuteUseCase(IMinuteRepository repository) : IDeleteMinuteUseCase
{
    public ICustomResult Handle(int id, int userId)
    {
        var result = new CustomResult();

        var timeMinute = repository.FindById(id, userId);
        if (timeMinute == null) return result.SetError(MinuteMessageErrors.NotFound);

        repository.Delete(timeMinute);
        return result;
    }
}