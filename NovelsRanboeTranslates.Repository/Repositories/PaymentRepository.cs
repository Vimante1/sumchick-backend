using MongoDB.Driver;
using NovelsRanboeTranslates.Domain.Models;
using NovelsRanboeTranslates.Repository.Interfaces;

namespace NovelsRanboeTranslates.Repository.Repositories;

public class PaymentRepository : BaseRepository<TransactionLog>, IPaymentRepository
{
    public PaymentRepository(IMongoDbSettings settings) : base(settings, "Payment") { }

    public async Task<bool> AddPaymentLog(TransactionLog transaction)
    {
        try
        {
            await _collection.InsertOneAsync(transaction);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> PaymentAlreadyContain(string TransactionId, string PaymentToken)
    {
        try
        {
            var filter = Builders<TransactionLog>.Filter.Eq("TransactionId", TransactionId);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result != null;
        }
        catch (Exception e)
        {
            return true;
        }
    }
}