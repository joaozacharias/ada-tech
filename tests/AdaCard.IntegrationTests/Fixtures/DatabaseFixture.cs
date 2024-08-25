using Testcontainers.PostgreSql;

namespace AdaCard.IntegrationTests.Fixtures;

public class DatabaseFixture : IAsyncLifetime
{
    private PostgreSqlContainer container;

    public string ConnectionString => container.GetConnectionString();

    public async Task InitializeAsync()
    {
        var containerBuilder = new PostgreSqlBuilder();

        this.container = containerBuilder.Build();

        await this.container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if(this.container != null)
        {
            await this.container.DisposeAsync();
        }
    }
}
