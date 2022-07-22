namespace Api.Models.Response;
public class FibonacciNumberResponse
{
    public string FibonacciNumber { get; set; }

    public FibonacciNumberResponse(string value)
    {
        FibonacciNumber = value;
    }
}