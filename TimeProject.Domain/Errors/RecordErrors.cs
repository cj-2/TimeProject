namespace TimeProject.Domain.Errors;

public static class RecordErrors
{
    public const string NotFound = "not_found:record_not_found";
    public const string AlreadyInUse = "bad_request:record_code_already_in_use";
    public const string CategoryNotFound = "bad_request:category_not_found";
    public const string CodeMustValue = "bad_request:record_code_must_value";
    public const string CodeAlreadyInUse = "bad_request:record_code_already_in_use";
}