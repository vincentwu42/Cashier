using Cashier.Data;
using Cashier.Models;
using Cashier.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Cashier.Pages.Account
{
    //[Authorize(Roles = "Admin")]
    public class Register : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityUser> _roleManager;

        [BindProperty]
        public User User { get; set; }
        public string ConfirmPassword {  get; set; }
        

        public Register(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityUser> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = new IdentityUser { UserName = User.Username };
            var result = await _userManager.CreateAsync(user, User.Password);

            if (result.Succeeded)
            {
                await _roleManager.SetRoleNameAsync(user, "Admin");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }







        //private readonly ApplicationDbContext _context;


        //[BindProperty]
        //public User User { get; set; }

        //[BindProperty]
        //public string ConfirmPassword { get; set; }
        //public List<UserRole> Roles { get; set; }

        //public Register(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task OnGetAsync()
        //{
        //    User = new User();
        //    Roles = await _context.UserRoles.ToListAsync();
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (await _context.Users.AnyAsync(u => u.Username == User.Username))
        //        {
        //            ModelState.AddModelError("LoginModel.Username", "Username already exists.");
        //            return Page();
        //        }

        //        if (User.Password != ConfirmPassword)
        //        {
        //            ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
        //            return Page();
        //        }

        //        User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);

        //        _context.Users.Add(User);
        //        await _context.SaveChangesAsync();

        //        return RedirectToPage("/Account/Login");
        //    }

        //    Roles = await _context.UserRoles.ToListAsync();
        //    return Page();
        //}
    }
}
