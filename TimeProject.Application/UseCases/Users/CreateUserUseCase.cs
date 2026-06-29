using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Dtos.Users;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class CreateUserUseCase(
    IUnitOfWork unitOfWork,
    IUserMapDataUtil mapper,
    IJwtHandler jwtHandler,
    ICreateOrUpdateUserPasswordUseCase createUserPasswordUseCase
) : ICreateUserUseCase
{
    public ICustomResult<CreateUserResult> Handle(ICreateUserDto dto)
    {
        var result = new CustomResult<CreateUserResult>();
        var emailAvailable = unitOfWork.UserRepository.EmailIsAvailable(dto.Email);

        if (emailAvailable == false) return result.SetError(UserMessageErrors.EmailAlreadyInUse);

        var entity = unitOfWork.UserRepository.Create(new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Timezone = dto.Timezone
        });

        unitOfWork.SaveChanges();

        createUserPasswordUseCase.Handle(entity.UserId,
            new CreatePasswordDto { Password = dto.Password },
            saveChanges: false);

        unitOfWork.SaveChanges();
        
        result.Data = new CreateUserResult
        {
            User = mapper.Handle(entity),
            Jwt = jwtHandler.Generate(entity)
        };

        // hookHandler.Send(HookTo.Users,
        //     $"<b>{dto.Name}</b> acabou de criar uma conta com o email:\n<b>{dto.Email}</b>");

        return result;
    }
}