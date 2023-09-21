using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Services.Interfraces;

public interface IPaymentService
{
    Task<bool> AddToLogs(TransactionLog log);
    Task<bool> PaymentAlreadyContain(string transactionId, string token);
}