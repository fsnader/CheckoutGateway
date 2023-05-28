using CheckoutGateway.Infrastructure.Repositories.Abstractions;

namespace CheckoutGateway.Domain;

public class Payment
{
    public Guid Id { get; set; }
    public Guid BankExternalId { get; set; }
    public Guid MerchantId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public CreditCard Card { get; set; }

    private async Task UpdateStatus(PaymentStatus status, string reason, IPaymentsRepository repository)
    {
        await repository.UpdatePaymentStatusAsync(this, status, reason);
        Status = status;
    }

    public async Task UpdateToDeclined(string reason, IPaymentsRepository repository)
        => await UpdateStatus(PaymentStatus.Declined, reason, repository);
    
    public async Task UpdateToProcessed(IPaymentsRepository repository)
        => await UpdateStatus(PaymentStatus.Processed, "Payment processed", repository);
}