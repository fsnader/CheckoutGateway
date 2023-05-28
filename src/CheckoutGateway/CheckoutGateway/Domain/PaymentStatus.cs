namespace CheckoutGateway.Domain;

public enum PaymentStatus
{
    Created,
    Authorized,
    InProgress,
    Declined,
    Processed
}