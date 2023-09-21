namespace NovelsRanboeTranslates.Domain.Models;

public class TransactionLog
{
    public string? Platform { get; set; }
    public string? PayerLogin { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentToken{ get; set; }
    public decimal? Amount { get; set; }
    public string AdditionalInformation{ get; set; }

}