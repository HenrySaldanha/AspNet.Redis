namespace Api.Models.Response;
public class IndicatorResponse
{
    public IndicatorResponse((string key, string value) item)
    {
        (Key, Value) = (item.key, item.value);
    }
    public string Key { get; set; }
    public string Value { get; set; }
}