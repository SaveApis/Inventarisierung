using System.Globalization;
using Microsoft.Extensions.Configuration;
using Testcontainers.MySql;
using Testcontainers.Redis;

namespace Inventarisierung.Tests;

public static class ContainerFixture
{
    static ContainerFixture()
    {
        MySqlContainer ??= new MySqlBuilder()
            .WithImage("mysql:9.2.0")
            .WithName("Inventarisierung-Test-MySql")
            .WithEnvironment("MYSQL_ROOT_PASSWORD", "root")
            .WithPortBinding(3306, true)
            .WithReuse(false)
            .Build();
        RedisContainer ??= new RedisBuilder()
            .WithImage("redis:8.0-M03-alpine3.21")
            .WithName("Inventarisierung-Test-Redis")
            .WithPortBinding(6379, true)
            .WithReuse(false)
            .Build();
    }

    private static MySqlContainer? MySqlContainer { get; set; }
    private static RedisContainer? RedisContainer { get; set; }

    public static void Start()
    {
        if (MySqlContainer is null)
        {
            throw new InvalidOperationException("MySql container is not initialized");
        }
        if (RedisContainer is null)
        {
            throw new InvalidOperationException("Redis container is not initialized");
        }

        Task.WaitAll(MySqlContainer.StartAsync(), RedisContainer.StartAsync());
    }

    public static void Stop()
    {
        if (MySqlContainer is null)
        {
            throw new InvalidOperationException("Container is not initialized");
        }
        if (RedisContainer is null)
        {
            throw new InvalidOperationException("Container is not initialized");
        }
        Task.WaitAll(MySqlContainer.StopAsync(), RedisContainer.StopAsync());
        Task.WaitAll(MySqlContainer.DisposeAsync().AsTask(), RedisContainer.DisposeAsync().AsTask());
    }

    public static IConfiguration LoadConfiguration()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        configuration["database_sql_name"] = "Inventarisierung-Test";
        configuration["database_sql_server"] = MySqlContainer?.Hostname;
        configuration["database_sql_port"] = MySqlContainer?.GetMappedPublicPort(3306).ToString(CultureInfo.InvariantCulture);
        configuration["database_sql_database"] = Guid.NewGuid().ToString().Replace("-", string.Empty).Trim();
        configuration["database_sql_user"] = "root";
        configuration["database_sql_password"] = "root";

        configuration["hangfire_redis_server"] = RedisContainer?.Hostname;
        configuration["hangfire_redis_port"] = RedisContainer?.GetMappedPublicPort(6379).ToString(CultureInfo.InvariantCulture);
        configuration["hangfire_redis_database"] = "0";
        configuration["hangfire_redis_prefix"] = "inventarisierung:test:hangfire:";

        return configuration;
    }
}
