using System.Text.Json;
using DotnetRedisApi;
using StackExchange.Redis;

public class RedisCacheService
{
    private readonly IConnectionMultiplexer redis;
    private readonly IDatabase db;
    public RedisCacheService(string connectionString)
    {
        redis = ConnectionMultiplexer.Connect(connectionString);
        db = redis.GetDatabase();
    }

    public T? Get<T>(string key)
    {
        var value = db.StringGet(key);
        if (!value.HasValue)
        {
            return default;
        }
        return JsonSerializer.Deserialize<T>(value);
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(value, options);
        db.StringSet(key, json, expiry);
    }

}