using Domain;
using Flurl.Http;
using Serilog;

namespace ExternalInterfaces
{
    public class GitHubApi
    {
        public async Task<GitHubRepository> GetRepositoriesByUser(string userName)
        {
            Log.Debug("Service: {0} Method: {1} Request: {2}",
                nameof(GitHubApi), nameof(GetRepositoriesByUser), userName);

            var url = $"http://api.github.com/users/{userName}/repos";
            try
            {
                var text = await url
                    .WithHeader("User-Agent", "MyRedisApp")
                    .AllowHttpStatus("403")
                    .GetAsync();

                var responseMessage = text.ResponseMessage;


            }
            catch (Exception e)
            {

            }
            //TODO: implement this
            throw new NotImplementedException();
        }
    }
}