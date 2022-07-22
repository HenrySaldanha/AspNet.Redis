namespace Application.Helpers;
public static class KeysHelper
{
    public static string GetFibonacciKey(uint n) => $"FIBONACCI:{n}";
    public static string GetLatestRepositoriesKey(string userName) => $"GITHUB_LATEST_REPOS:{userName}";
    public static string GetMostStarredRepositoryKey(string userName) => $"GITHUB_MOST_STAR_REPOS:{userName}";
}