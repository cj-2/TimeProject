namespace TimeProject.Domain.Errors;

public static class MinuteMessageErrors
{
    public const string Required = "bad_request:record_id_or_category_id_is_required";
    public const string NotFound = "not_found:minute_not_found";
}