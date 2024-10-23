using CalculatorServices;
using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Cashier.Pages
{
    public class Transaction : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CalculatorSoapClient _calculatorServices;


        [BindProperty]
        public Cashier.Models.Product Product { get; set; }
        public List<TransactionDetail> transactionDetails { get; set; }
        public string TransactionID { get; set; }

        public Transaction(ApplicationDbContext context)
        {
            _context = context;
            //_calculatorServices = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
        }

        public async Task OnGetAsync()
        {
            GetCartSession();

            Product = new Product();
        }

        public async Task<IActionResult> OnPost()
        {
            GetCartSession();

            var existingItem = transactionDetails.FirstOrDefault(x => x.ProductID == Product.ID);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                transactionDetails.Add(new TransactionDetail
                {
                    ProductID = Product.ID,
                    UnitPrice = Product.ProductPrice,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetString("TransactionDetails", JsonConvert.SerializeObject(transactionDetails));
            return RedirectToPage("/Transaction"); // Redirect to a success page or another page
        }

        public IActionResult OnPostSubmitCart()
        {
            // Here you can handle the submission of the cart to the database
            // e.g., save to your database using Entity Framework

            return OnPostClearSession();
        }

        public IActionResult OnPostClearSession()
        {
            HttpContext.Session.Remove("TransactionDetails");
            return RedirectToPage("/Transaction");
        }

        void GetCartSession()
        {
            var jsonData = HttpContext.Session.GetString("TransactionDetails");
            if (jsonData != null)
            {
                transactionDetails = JsonConvert.DeserializeObject<List<TransactionDetail>>(jsonData) ?? new List<TransactionDetail>();
            }
            else
            {
                transactionDetails = new List<TransactionDetail>();
            }
        }
    }
}
