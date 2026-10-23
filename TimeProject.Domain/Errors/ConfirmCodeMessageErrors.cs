namespace TimeProject.Domain.Errors;

public static class ConfirmCodeMessageErrors
{
    public const string CheckYourEmailInbox = "bad_request:check_your_email_inbox";
    public const string NotFound = "bad_request:recovery_code_not_found";
    public const string IsUsedOrExpired = "bad_request:recovery_code_is_used_or_expired";
}