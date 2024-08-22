namespace AdaCard.Api.Contracts;

public class Error
{
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Exception { get; set; }
}
