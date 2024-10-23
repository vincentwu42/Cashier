using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cashier.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Foreign key relationship
        public int RoleID { get; set; }
        [NotMapped]
        [ValidateNever]
        public UserRole UserRole { get; set; }
    }
}
