using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Pages.Account
{
    public class Login : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public User User { get; set; }

        public Login (SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            User = new User();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(User.Username, User.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }





        //private readonly ApplicationDbContext _context;

        //[BindProperty]
        //public User User { get; set; }

        //public Login(ApplicationDbContext context)
        //{
        //    _context = context;
        //    User = new User();
        //}

        //public void OnGet()
        //{

        //}

        //public async Task<IActionResult> OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var existingUser = await _context.Users
        //        .FirstOrDefaultAsync(u => u.Username == User.Username);

        //    if (existingUser == null || !BCrypt.Net.BCrypt.Verify(User.Password, existingUser.Password))
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        return Page();
        //    }

        //    return RedirectToPage("/Index");
        //}
    }
}
