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
        RETURNING 
            id, 
            merchant_id,
            bank_external_id, 
            amount, 
            currency, 
            status,
            card_name as name, 
            card_number as number, 
            card_scheme as scheme,
            card_expiration_month as month, 
            card_expiration_year as year, 
            card_cvv as cvv
";
}