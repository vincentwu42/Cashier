namespace Cashier.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation property
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
