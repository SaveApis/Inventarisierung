using System.Globalization;
using Microsoft.Extensions.Configuration;
using Testcontainers.MySql;

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
    }

    private static MySqlContainer? MySqlContainer { get; set; }

    public static async Task StartAsync()
    {
        if (MySqlContainer is null)
        {
            throw new InvalidOperationException("Container is not initialized");
        }

        await MySqlContainer.StartAsync().ConfigureAwait(false);
    }

    public static async Task StopAsync()
    {
        if (MySqlContainer is null)
        {
            throw new InvalidOperationException("Container is not initialized");
        }

        await MySqlContainer.StopAsync().ConfigureAwait(false);
        await MySqlContainer.DisposeAsync().ConfigureAwait(false);
    }

    public static IConfiguration LoadConfiguration(string databaseName)
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        configuration["database_sql_name"] = "Inventarisierung-Test";
        configuration["database_sql_server"] = MySqlContainer?.Hostname;
        configuration["database_sql_port"] = MySqlContainer?.GetMappedPublicPort(3306).ToString(CultureInfo.InvariantCulture);
        configuration["database_sql_database"] = databaseName;
        configuration["database_sql_user"] = "root";
        configuration["database_sql_password"] = "root";

        return configuration;
    }
}
