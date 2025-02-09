using System.Globalization;
using Microsoft.Extensions.Configuration;
using Testcontainers.MySql;

namespace Backend.Tests.Fixtures;

public class ContainerFixture
{
    private MySqlContainer MySqlContainer { get; } = new MySqlBuilder()
        .WithImage("mysql:9.2.0")
        .WithEnvironment("MYSQL_ROOT_PASSWORD", "root")
        .WithName("inventarisierung-tests-mysql")
        .WithPortBinding(3306, true)
        .WithReuse(true)
        .Build();

    public async Task StartAsync()
    {
        await MySqlContainer.StartAsync().ConfigureAwait(false);
    }

    public async Task StopAsync()
    {
        await MySqlContainer.StopAsync().ConfigureAwait(false);
        await MySqlContainer.DisposeAsync().ConfigureAwait(false);
    }

    public IConfiguration LoadConfiguration(string name)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection()
            .Build();

        configuration["database_sql_name"] = "inventarisierung-Tests";
        configuration["database_sql_server"] = MySqlContainer.Hostname;
        configuration["database_sql_port"] = MySqlContainer.GetMappedPublicPort(3306).ToString(CultureInfo.InvariantCulture);
        configuration["database_sql_database"] = name;
        configuration["database_sql_user"] = "root";
        configuration["database_sql_password"] = "root";

        return configuration;
    }
}
