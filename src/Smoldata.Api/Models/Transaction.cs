namespace Smoldata.Api.Models
{
    public class Transaction
    {
        public string BuyerAccount { get; set; }
        public string SellerAccount { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
