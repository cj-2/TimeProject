namespace TimeProject.Domain.Errors;

public static class AuthMessageErrors
{
    public const string WrongEmailOrPassword = "bad_request:wrong_email_or_password";
    public const string SendEmailError = "server_error:send_recovery_email_error";
    public const string PasswordLoginDisabled = "bad_request:password_login_disabled";
    public const string AuthProviderError = "server_error:auth_provider_error";
    public const string CreateUserError = "server_error:create_user_error";
}