using FluentResults;

namespace AdaCard.Api.Contracts.Mappers;

public static class ErrorMapper
{
    private static Func<IEnumerable<IError>, Error> Map = (x) =>
    {
        return new Error
        {
            Exception = "",
            Message = string.Join(", ", x.Select(x => x.Message)),
            StatusCode = 400
        };
    };

    public static Error MapToErrror(this IEnumerable<IError> errorResult)
    {
        return Map(errorResult);
    }
}
