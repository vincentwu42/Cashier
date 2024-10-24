using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cashier.Pages
{
    //[Authorize(Roles = "Admin")]
    public class Products : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Product> ProductLists;

        public Products(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ProductLists = await _context.Products
                .Include(p => p.ProductStocks)
                .Select(p => new Product
                {
                    ID = p.ID,
                    ProductName = p.ProductName,
                    ProductDesc = p.ProductDesc,
                    ProductPrice = p.ProductPrice,
                    TotalQuantity = p.ProductStocks.Sum(ps => ps.ProductQty)
                })
                .ToListAsync();
        }
    }
}
