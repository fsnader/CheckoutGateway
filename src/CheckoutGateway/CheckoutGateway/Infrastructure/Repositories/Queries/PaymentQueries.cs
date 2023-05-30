namespace CheckoutGateway.Infrastructure.Repositories.Queries;

public static class PaymentQueries
{
    public const string Create = @"
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
             card_cvv,
             created_at,
             updated_at)
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
             @CardCvv,
             @CreatedAt,
             @UpdatedAt)
        RETURNING 
            id, 
            merchant_id,
            bank_external_id, 
            amount, 
            currency, 
            status,
            created_at,
            updated_at,
            card_name as name, 
            card_number as number, 
            card_scheme as scheme,
            card_expiration_month as month, 
            card_expiration_year as year, 
            card_cvv as cvv;
";

    public const string UpdateById = @"
        UPDATE payment SET
            merchant_id = @MerchantId, 
            bank_external_id = @BankExternalId,
            amount = @Amount, 
            currency = @Currency, 
            status = @Status::payment_status, 
            card_name = @CardName, 
            card_number = @CardNumber, 
            card_scheme = @CardScheme, 
            card_expiration_month = @CardExpirationMonth, 
            card_expiration_year = @CardExpirationYear, 
            card_cvv = @CardCvv,
            updated_at = @UpdatedAt
        WHERE id = @Id;
";

    public const string GetById = @"
        SELECT
            id, 
            merchant_id,
            bank_external_id, 
            amount, 
            currency, 
            status,
            created_at,
            updated_at,
            card_name as name, 
            card_number as number, 
            card_scheme as scheme,
            card_expiration_month as month, 
            card_expiration_year as year, 
            card_cvv as cvv
        FROM payment
        WHERE id = @Id;
";
    public const string ListByMerchantId = @"
        SELECT
            id, 
            merchant_id,
            bank_external_id, 
            amount, 
            currency, 
            status,
            created_at,
            updated_at,
            card_name as name, 
            card_number as number, 
            card_scheme as scheme,
            card_expiration_month as month, 
            card_expiration_year as year, 
            card_cvv as cvv
        FROM payment
        WHERE merchant_id = @MerchantId;
";
}