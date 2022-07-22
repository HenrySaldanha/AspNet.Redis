namespace Application.IServices;
public interface IIndicatorsService
{
    public Task<(string, string)> InsertOrUpdateAsync(string key, string value);
    public Task<string> GetAsync(string key);
    public Task DeleteAsync(string key);
}
