using System.ComponentModel.DataAnnotations;

namespace Cashier.Models
{
    public class TransactionDetail
    {
        public int ID { get; set; }
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public Transaction Transaction { get; set; }
        public Product Product { get; set; }
    }
}
