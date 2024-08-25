using AdaCard.Core.Features.Cards.Commands;
using AdaCard.Core.Features.Cards.DTOs;
using AdaCard.Core.Features.Cards.Validators;

using FluentAssertions;

namespace AdaCard.UnitTests.Core.Features.Validators;

public class UpdateCardCommandValidatorTests
{
    private readonly UpdateCardCommandValidator validator;
    
    public UpdateCardCommandValidatorTests()
    {
        this.validator = new UpdateCardCommandValidator();
    }

    [Theory]
    [InlineData("Titulo", "Conteudo", null, "Lista é obrigatório!")]
    [InlineData("Titulo", "", "Lista", "Conteúdo é obrigatório!")]
    [InlineData(null, "Conteudo", "Lista", "Título é obrigatório!")]
    public async Task Validate_InvalidRequest_ShouldFail(string? titulo, string? conteudo, string? lista, string errorMessage)
    {
        // Arrange
        var command = new UpdateCardCommand(Guid.NewGuid(), new CreateCard
        {
            Conteudo = conteudo,
            Lista = lista,
            Titulo = titulo
        });

        // Act
        var result = await this.validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors[0].ErrorMessage.Should().Be(errorMessage);
    }

    [Fact]
    public async Task Validate_ValidRequest_ShouldPass()
    {
        // Arrange
        var command = new UpdateCardCommand(Guid.NewGuid(), new CreateCard
        {
            Conteudo = "ConteudoTeste",
            Lista = "Lista",
            Titulo = "TituloTeste"
        });

        // Act
        var result = await this.validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_MissingId_ShouldFail()
    {
        // Arrange
        var command = new UpdateCardCommand(default, new CreateCard
        {
            Conteudo = "ConteudoTeste",
            Lista = "Lista",
            Titulo = "TituloTeste"
        });

        // Act
        var result = await this.validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors[0].ErrorMessage.Should().Be("ID é obrigatório!");
    }
}
