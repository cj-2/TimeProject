using TimeProject.Application.Modules.Codes.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.Modules.Codes.UseCases;

public class CreateConfirmCodeUseCase(IConfirmCodeRepository repository) : ICreateConfirmCodeUseCase
{
    public ICustomResult<ConfirmCode> Handle(int userId, ConfirmCodeType type)
    {
        var result = new CustomResult<ConfirmCode>();
        var codes = repository.FindByUserIdThatIsNotExpiredOrUsed(userId, type);

        return codes.Count > 0
            ? result.SetData(codes.First())
            : result.SetData(repository.Create(new ConfirmCode
            {
                UserId = userId,
                Expiration = DateTime.Now.AddMinutes(15).ToUniversalTime(),
                Type = type
            }));
    }
}