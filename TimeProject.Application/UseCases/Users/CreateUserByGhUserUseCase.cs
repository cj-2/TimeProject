using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users;

public class CreateUserByGhUserUseCase(IUnitOfWork unitOfWork) : ICreateUserByGhUserUseCase
{
    public ICustomResult<User> Handle(CreateUserOAuthDto dto, IEnumerable<EmailGh> emails)
    {
        var result = new CustomResult<User>();

        if (string.IsNullOrEmpty(dto.UserProviderId))
            return result.SetError(UserMessageErrors.OAuthWithoutProviderId);

        var filtredEmails = emails.Where(e => e.Verified).ToList();
        var primaryEmail = filtredEmails.FirstOrDefault(e => e.Primary);

        if (filtredEmails.Count != 0 && primaryEmail != null)
        {
            var userWithPrimaryEmail = unitOfWork.UserRepository.FindByEmail(primaryEmail.Email);

            if (userWithPrimaryEmail != null)
            {
                unitOfWork.UserProviderRepository.Create(new UserProvider
                {
                    UserId = userWithPrimaryEmail.UserId,
                    Provider = "github",
                    ExternalId = dto.UserProviderId
                });
                
                unitOfWork.SaveChanges();

                return result.SetData(userWithPrimaryEmail);
            }

            filtredEmails.Remove(primaryEmail);

            foreach (var secondaryEmail in filtredEmails)
            {
                var userWithSecondaryEmail = unitOfWork.UserRepository.FindByEmail(secondaryEmail.Email);
                if (userWithSecondaryEmail == null) continue;

                unitOfWork.UserProviderRepository.Create(new UserProvider
                {
                    UserId = userWithSecondaryEmail.UserId,
                    Provider = "github",
                    ExternalId = dto.UserProviderId
                });

                unitOfWork.SaveChanges();

                return result.SetData(userWithSecondaryEmail);
            }
        }

        var userEntity = unitOfWork.UserRepository
            .Create(new User
            {
                Name = dto.Name,
                Email = primaryEmail?.Email ?? "",
                Timezone = ""
            });

        unitOfWork.SaveChanges();

        unitOfWork.UserProviderRepository.Create(new UserProvider
        {
            UserId = userEntity.UserId,
            Provider = "github",
            ExternalId = dto.UserProviderId
        });

        unitOfWork.SaveChanges();

        return result.SetData(userEntity);
    }
}