namespace Application.Options;
public static class KeysHelper
{
    private const string FIBONACCI = $"FIBONACCI";

    public static string GetFibonacciKey(int n)
    {
        return $"{FIBONACCI}:{n}";
    }
}