using CheckoutGateway.Application.UseCases.Payments;

namespace CheckoutGateway.UnitTests.Application.UseCases.Payments;

public class GetPaymentByIdTests
{
    private readonly Mock<IPaymentsRepository> _paymentsRepository;
    private readonly GetPaymentById _sut;

    public GetPaymentByIdTests()
    {
        var autoMocker = new AutoMocker();
        _paymentsRepository = autoMocker.GetMock<IPaymentsRepository>();
        _sut = autoMocker.CreateInstance<GetPaymentById>();
    }

    [Fact]
    public async Task ExecuteAsync_InputValid_Returns_SuccessResult()
    {
        // Arrange
        var merchantId = Guid.NewGuid();
        
        _paymentsRepository.Setup(r => r.GetByIdAsync(
                It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Payment { MerchantId = merchantId});

        // Act
        var result = await _sut.ExecuteAsync(Guid.NewGuid(), merchantId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
    }
    
    [Fact]
    public async Task ExecuteAsync_InputValid_Returns_NotFoundResult_For_Different_MerchantId()
    {
        // Arrange
        var merchantId = Guid.NewGuid();
        
        _paymentsRepository.Setup(r => r.GetByIdAsync(
                It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Payment { MerchantId = Guid.NewGuid()});

        // Act
        var result = await _sut.ExecuteAsync(Guid.NewGuid(), merchantId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ErrorType.NotFound, result.Error);
        Assert.Null(result.Result);
    }
    
    [Fact]
    public async Task ExecuteAsync_InputValid_Returns_NotFoundResult()
    {
        // Arrange
        var merchantId = Guid.NewGuid();

        // Act
        var result = await _sut.ExecuteAsync(Guid.NewGuid(), merchantId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ErrorType.NotFound, result.Error);
        Assert.Null(result.Result);
    }
}