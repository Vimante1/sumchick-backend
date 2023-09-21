using System.Security.Cryptography;
using System.Text;

namespace NovelsRanboeTranslates.Domain.Models;

public class AdvCashStatus
{
    public decimal ac_amount { get; set; }
    public string ac_merchant_currency { get; set; }
    public string ac_start_date {get; set; }
    public string ac_sci_name {get; set; }
    public string ac_src_wallet {get; set; }
    public string ac_dest_wallet {get; set; }
    public int ac_order_id { get; set; }
    public string ac_transfer { get; set; }
    public decimal ac_buyer_amount_without_commission { get; set; }
    public string ac_comments { get; set; }
    public string ac_hash { get; set; }
    public string userLogin { get; set; }

    public string getHash(string password)
    {
        string unhash = this.ac_transfer + ':' + this.ac_start_date + ':' + this.ac_sci_name + ':' + this.ac_src_wallet + ':' +
                        this.ac_dest_wallet + ':' + this.ac_order_id + ':' + this.ac_amount + ':' + this.ac_merchant_currency + ':' +
                        password;

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(unhash);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                builder.Append(b.ToString("x2")); // Перетворюємо байти у шістнадцятковий формат
            }

            return builder.ToString();
        }
    }
}