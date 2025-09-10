using StackExchange.Redis;
namespace checkpoint01.Services
{
        public class RedisService
        {
            private readonly IDatabase _database;

            public RedisService(string connectionString)
            {
                var connection = ConnectionMultiplexer.Connect(connectionString);
                _database = connection.GetDatabase();
            }

            public async Task<string> ObterCacheAsync(string key)
            {
                return await _database.StringGetAsync(key);
            }

            public async Task SetCacheAsync(string key, string value, TimeSpan expiration)
            {
                await _database.StringSetAsync(key, value, expiration);
            }
        }

    }
