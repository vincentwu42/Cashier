using System.ComponentModel.DataAnnotations.Schema;

namespace Cashier.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }

        [NotMapped]
        public int TotalQuantity { get; set; }
        // Navigation property
        public ICollection<ProductStock> ProductStocks { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
