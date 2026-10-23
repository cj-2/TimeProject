namespace TimeProject.Domain.Errors;

public static class UserErrors
{
    public static readonly string NotFound = ErrorTypes.BadRequest.Message("user_not_found");
    public static readonly string EmailAlreadyInUse = ErrorTypes.BadRequest.Message("email_already_in_use");
    public static readonly string DifferentPassword = ErrorTypes.BadRequest.Message("different_password");
    public static readonly string RoleNotFound = ErrorTypes.BadRequest.Message("role_not_found");
    public static readonly string PasswordNotAllowed = ErrorTypes.Unauthorized.Message("password_not_allowed");
    public static readonly string Unauthorized = ErrorTypes.Unauthorized.Message("unauthorized");
    public static readonly string TimeZoneNotFound = ErrorTypes.BadRequest.Message("time_zone_not_found");
    public static readonly string OAuthWithoutProviderId = ErrorTypes.ServerError.Message("you_need_a_provider_id");

    public static readonly string UserEmailProviderNotVerified =
        ErrorTypes.BadRequest.Message("user_email_provider_not_verified");
}