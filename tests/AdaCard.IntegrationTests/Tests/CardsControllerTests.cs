using System.Net.Http.Json;

using AdaCard.Core.Features.Cards.DTOs;
using AdaCard.Core.Features.Login.DTOs;
using AdaCard.IntegrationTests.Fixtures;

using FluentAssertions;

namespace AdaCard.IntegrationTests.Tests;

[Collection(InfrastructureCollection.Name)]
public class CardsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory factory;

    public CardsControllerTests(CustomWebApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task PostCard_ValidRequest_ShouldReturnCardWithId()
    {
        // Arrange
        using var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(300));

        var client = factory.CreateClient();

        string token = await LoginAsync(client, cancellationToken);

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var request = new CreateCard
        {
            Conteudo = "TesteConteudo",
            Lista = "Lista",
            Titulo = "TituloTeste"
        };

        // Act
        var response = await client.PostAsJsonAsync("cards", request, cancellationToken.Token);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Card>(cancellationToken.Token);
        result.Should().BeEquivalentTo(request);
        result!.Id.Should().NotBe(null);
    }

    private static async Task<string> LoginAsync(HttpClient client, CancellationTokenSource cancellationToken)
    {
        var loginRequest = new AuthenticationRequest
        {
            Login = "test",
            Senha = "test"
        };

        var loginResponse = await client.PostAsJsonAsync("login", loginRequest, cancellationToken.Token);

        var token = await loginResponse.Content.ReadAsStringAsync();
        return token;
    }
}
