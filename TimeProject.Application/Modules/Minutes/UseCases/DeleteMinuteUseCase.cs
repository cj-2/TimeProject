using TimeProject.Application.Shared;
using TimeProject.Application.Modules.Minutes.Interfaces;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Minutes.UseCases;

public class DeleteMinuteUseCase(IMinuteRepository repository) : IDeleteMinuteUseCase
{
    public ICustomResult Handle(int id, int userId)
    {
        var result = new CustomResult();

        var timeMinute = repository.FindById(id, userId);
        if (timeMinute == null) return result.SetError(MinuteErrors.NotFound);

        repository.Delete(timeMinute);
        return result;
    }
}