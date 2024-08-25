using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AdaCard.IntegrationTests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DatabaseFixture databaseFixture;

    public CustomWebApplicationFactory(DatabaseFixture databaseFixture)
    {
        this.databaseFixture = databaseFixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        //Se tivesse uma base de dados aqui faria a substituição da connection string, por exemplo:
        builder.UseSetting("ConnectionStrings:AdaCards", this.databaseFixture.ConnectionString);

        builder.UseSetting("AuthenticationConfigurations:Secret", Guid.NewGuid().ToString());
        builder.UseSetting("UserDefault:Login", "test");
        builder.UseSetting("UserDefault:Senha", "test");
    }
}
