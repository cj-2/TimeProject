using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Interfaces.UseCases.Codes;

public interface ICreateConfirmCodeUseCase
{
    ICustomResult<IConfirmCode> Handle(int userId, ConfirmCodeType type);
}