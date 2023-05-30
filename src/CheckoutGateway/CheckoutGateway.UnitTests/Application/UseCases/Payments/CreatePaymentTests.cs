using CheckoutGateway.Application.UseCases.Payments;
using CheckoutGateway.Infrastructure.Gateways;
using FluentValidation;
using FluentValidation.Results;

namespace CheckoutGateway.UnitTests.Application.UseCases.Payments;

public class CreatePaymentTests
{
    private readonly Mock<IPaymentsRepository> _paymentsRepository;
    private readonly Mock<IBankGateway> _bankGateway;
    private readonly CreatePayment _sut;
    private readonly Mock<IValidator<Payment>> _validator;

    public CreatePaymentTests()
    {
        var autoMocker = new AutoMocker();
        _paymentsRepository = autoMocker.GetMock<IPaymentsRepository>();
        _validator = autoMocker.GetMock<IValidator<Payment>>();
        _bankGateway = autoMocker.GetMock<IBankGateway>();
        _sut = autoMocker.CreateInstance<CreatePayment>();
    }

    [Fact]
    public async Task InvalidInput_Returns_BadRequestResult()
    {
        // Arrange
        _validator.Setup(v => 
                v.ValidateAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("aa", "bb") }));
        
        // Act
        var result = await _sut.ExecuteAsync(new Payment(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ErrorType.BadRequest, result.Error);
        Assert.Null(result.Result);
    }
    
    [Fact]
    public async Task ValidInput_Returns_SuccessResult()
    {
        // Arrange
        _validator.Setup(v => 
                v.ValidateAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _bankGateway.Setup(g =>
                g.ProcessPaymentAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = true
            });

        _paymentsRepository.Setup(r =>
                r.CreateAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Payment());
        
        // Act
        var result = await _sut.ExecuteAsync(new Payment(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
    }
    
    [Fact]
    public async Task ValidInput_Returns_RejectedResult_If_Gateway_Returns_Error()
    {
        // Arrange
        _validator.Setup(v => 
                v.ValidateAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        _paymentsRepository.Setup(r =>
                r.CreateAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Payment());

        _bankGateway.Setup(g =>
                g.ProcessPaymentAsync(It.IsAny<Payment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PaymentGatewayResponse
            {
                Id = Guid.NewGuid(),
                Success = false,
                Error = "This is an error"
            });

        // Act
        var result = await _sut.ExecuteAsync(new Payment(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ErrorType.Rejected, result.Error);
        Assert.Null(result.Result);
    }
}