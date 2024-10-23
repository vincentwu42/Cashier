using System.ComponentModel.DataAnnotations;

namespace Cashier.Models
{
    public class ProductStock
    {
        public int ID { get; set; }
        public int ProductID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
        public int ProductQty { get; set; }

        // Navigation property
        public Product Product { get; set; }
    }
}
