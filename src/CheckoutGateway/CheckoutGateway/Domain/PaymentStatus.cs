namespace CheckoutGateway.Domain;

public enum PaymentStatus
{
    Authorized,
    Pending,
    InProgress,
    Declined,
    Processed
}