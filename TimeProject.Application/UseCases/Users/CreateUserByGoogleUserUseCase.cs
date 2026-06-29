using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class CreateUserByGoogleUserUseCase(IUnitOfWork unitOfWork) : ICreateUserByGoogleUserUseCase
{
    public ICustomResult<IUser> Handle(CreateUserOAuthDto dto, string email)
    {
        var result = new CustomResult<IUser>();

        if (string.IsNullOrEmpty(dto.UserProviderId))
            return result.SetError(UserMessageErrors.OAuthWithoutProviderId);

        var userWithEmail = unitOfWork.UserRepository.FindByEmail(email);

        if (userWithEmail != null)
        {
            unitOfWork.UserProviderRepository.Create(new UserProvider
            {
                UserId = userWithEmail.UserId,
                Provider = "google",
                ExternalId = dto.UserProviderId
            });

            unitOfWork.SaveChanges();

            return result.SetData(userWithEmail);
        }

        var userEntity = unitOfWork.UserRepository
            .Create(new User
            {
                Name = dto.Name,
                Email = email,
                Timezone = ""
            });

        unitOfWork.SaveChanges();

        unitOfWork.UserProviderRepository.Create(new UserProvider
        {
            UserId = userEntity.UserId,
            Provider = "google",
            ExternalId = dto.UserProviderId
        });

        unitOfWork.SaveChanges();

        return result.SetData(userEntity);
    }
}