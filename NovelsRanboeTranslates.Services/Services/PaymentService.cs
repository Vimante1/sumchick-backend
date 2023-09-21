using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;
using NovelsRanboeTranslates.Services.Interfraces;

namespace NovelsRanboeTranslates.Services.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _repository = paymentRepository;
    }

    public async Task<bool> AddToLogs(TransactionLog log)
    {
        var result = await _repository.AddPaymentLog(log);

        return result;
    }

    public async Task<bool> PaymentAlreadyContain(string transactionId, string token)
    {
        var result = await _repository.PaymentAlreadyContain(transactionId, token);
        return result;
    }
}