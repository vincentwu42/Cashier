namespace Cashier.Models
{
    public class Report
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
    }
}
