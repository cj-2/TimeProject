namespace TimeProject.Domain.Errors;

public static class UserMessageErrors
{
    public const string NotFound = "not_found:user_not_found";
    public const string EmailAlreadyInUse = "bad_request:email_already_in_use";
    public const string DifferentPassword = "bad_request:different_password";
    public const string RoleNotFound = "bad_request:role_not_found";
    public const string PasswordNotAllowed = "unauthorized:password_not_allowed";
    public const string Unauthorized = "unauthorized:unauthorized";
    public const string OAuthWithoutProviderId = "server_error:you_need_a_provider_id";
    public const string UserEmailProviderNotVerified = "bad_request:user_email_provider_not_verified";
}