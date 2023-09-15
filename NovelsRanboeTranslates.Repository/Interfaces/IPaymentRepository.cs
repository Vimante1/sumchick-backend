using NovelsRanboeTranslates.Domain.Models;

namespace NovelsRanboeTranslates.Repository.Interfaces;

public interface IPaymentRepository
{
    Task<bool> AddPaymentLog(TransactionLog transaction);
    Task<bool> PaymentAlreadyContain(string TransactionId, string PaymentToken);
}