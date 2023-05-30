using CheckoutGateway.Application.UseCases.Payments;

namespace CheckoutGateway.UnitTests.Application.UseCases.Payments;

public class GetPaymentsListTests
{
    private readonly Mock<IPaymentsRepository> _paymentsRepository;
    private readonly GetPaymentsList _sut;

    public GetPaymentsListTests()
    {
        var autoMocker = new AutoMocker();
        _paymentsRepository = autoMocker.GetMock<IPaymentsRepository>();
        _sut = autoMocker.CreateInstance<GetPaymentsList>();
    }

    [Fact]
    public async Task ExecuteAsync_InputValid_Returns_SuccessResult()
    {
        // Arrange
        _paymentsRepository.Setup(r => r.ListByMerchantIdAsync(
                It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Payment[] {});


        // Act
        var result = await _sut.ExecuteAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
    }
}