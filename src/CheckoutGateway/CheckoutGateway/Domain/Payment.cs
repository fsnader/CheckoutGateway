using CheckoutGateway.Domain.Abstractions;

namespace CheckoutGateway.Domain;

public class Payment
{
    public Guid Id { get; set; }
    public Guid BankExternalId { get; set; }
    public Guid MerchantId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public CreditCard Card { get; set; }

    private async Task UpdateStatus(PaymentStatus status, string reason, IPaymentsRepository repository, CancellationToken cancellationToken)
    {
        Status = status;
        await repository.UpdatePaymentAsync(this, reason, cancellationToken);
    }

    public async Task UpdateToDeclined(string reason, IPaymentsRepository repository, CancellationToken cancellationToken)
        => await UpdateStatus(PaymentStatus.Declined, reason, repository, cancellationToken);
    
    public async Task UpdateToProcessed(Guid bankExternalId, IPaymentsRepository repository, CancellationToken cancellationToken)
    {
        BankExternalId = bankExternalId;
        await UpdateStatus(PaymentStatus.Processed, "Payment processed", repository, cancellationToken);
    }
}