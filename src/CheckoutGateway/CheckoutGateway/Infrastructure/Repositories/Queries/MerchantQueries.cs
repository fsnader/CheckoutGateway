namespace CheckoutGateway.Infrastructure.Repositories.Queries;

public static class MerchantQueries
{
    public const string Create = @"
        INSERT INTO merchant (name, client_id, secret_id)
        VALUES (@Name, @ClientId, @SecretId)
        RETURNING *
";

    public const string GetById = @"SELECT * FROM merchant WHERE client_id = @ClientId;";
}