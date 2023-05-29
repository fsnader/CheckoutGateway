namespace CheckoutGateway.Infrastructure.Repositories.Queries;

public static class PaymentQueries
{
    public const string CreatePayment = @"
        INSERT INTO payment
            (merchant_id, 
             amount, 
             currency, 
             status, 
             card_name, 
             card_number, 
             card_scheme, 
             card_expiration_month, 
             card_expiration_year, 
             card_cvv)
        VALUES
            (@MerchantId, 
             @Amount, 
             @Currency, 
             @Status::payment_status, 
             @CardName, 
             @CardNumber, 
             @CardScheme, 
             @CardExpirationMonth, 
             @CardExpirationYear, 
             @CardCvv)
        RETURNING *;
";
}