namespace Cashier.Models
{
    public class UserRole
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<User> Users { get; set; }
    }
}
