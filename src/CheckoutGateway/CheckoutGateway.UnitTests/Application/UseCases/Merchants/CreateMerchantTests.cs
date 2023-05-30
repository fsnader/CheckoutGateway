using CheckoutGateway.Application.UseCases.Merchants;
using CheckoutGateway.Application.UseCases.Merchants.Abstractions;

namespace CheckoutGateway.UnitTests.Application.UseCases.Merchants;

public class CreateMerchantTests
{
    private readonly Mock<IMerchantsRepository> _merchantsRepository;
    private readonly ICreateMerchant _sut;

    public CreateMerchantTests()
    {
        var autoMocker = new AutoMocker();
        _merchantsRepository = autoMocker.GetMock<IMerchantsRepository>();
        _sut = autoMocker.CreateInstance<CreateMerchant>();
    }

    [Fact]
    public async Task ExecuteAsync_InputValid_Returns_SuccessResult()
    {
        // Arrange
        var clientId = "client";
        var name = "name";

        _merchantsRepository.Setup(r => r.CreateAsync(
                It.IsAny<Merchant>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Merchant());

        // Act
        var result = await _sut.ExecuteAsync(name, clientId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
    }

    [Fact]
    public async Task ExecuteAsync_With_Existing_ClientId_Returns_BadRequestResult()
    {
        // Arrange
        var clientId = "client";
        var name = "name";

        _merchantsRepository.Setup(r => r.GetByClientId(
                clientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Merchant());

        // Act
        var result = await _sut.ExecuteAsync(name, clientId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ErrorType.BadRequest, result.Error);
        Assert.Null(result.Result);
    }
}