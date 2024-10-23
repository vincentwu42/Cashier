using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Pages.Users
{
    public class Login : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public Login(ApplicationDbContext context)
        {
            _context = context;
            User = new User();
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == User.Username);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(User.Password, existingUser.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
