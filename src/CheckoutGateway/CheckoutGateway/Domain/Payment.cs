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
        Status = status;
        await repository.UpdatePaymentAsync(this, reason);
    }

    public async Task UpdateToDeclined(string reason, IPaymentsRepository repository)
        => await UpdateStatus(PaymentStatus.Declined, reason, repository);
    
    public async Task UpdateToProcessed(Guid bankExternalId, IPaymentsRepository repository)
    {
        BankExternalId = bankExternalId;
        await UpdateStatus(PaymentStatus.Processed, "Payment processed", repository);
    }
}