using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Pages.Users
{
    public class Register : PageModel
    {
        private readonly ApplicationDbContext _context;


        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }
        public List<UserRole> Roles { get; set; }

        public Register(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            User = new User();
            Roles = await _context.UserRoles.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username == User.Username))
                {
                    ModelState.AddModelError("LoginModel.Username", "Username already exists.");
                    return Page();
                }

                if (User.Password != ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return Page();
                }

                User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);

                _context.Users.Add(User);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Users/Login");
            }

            Roles = await _context.UserRoles.ToListAsync();
            return Page();
        }
    }
}
