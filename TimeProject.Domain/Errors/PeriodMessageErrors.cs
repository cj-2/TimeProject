namespace TimeProject.Domain.Errors;

public static class PeriodMessageErrors
{
    public const string EndDateIsBiggerThenStartDate = "bad_request:end_date_is_bigger_then_start_date";
    public const string NotFound = "not_found:period_not_found";
}