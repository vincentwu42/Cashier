using CalculatorServices;
using Cashier.Data;
using Cashier.Models;
using Cashier.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cashier.Pages
{
    //[Authorize(Roles = "Admin")]
    public class Report : PageModel
    {
        private readonly CalculatorSoapClient _calculatorServices;
        private readonly ApplicationDbContext _context;
        ExportExcelServices _exportExcel;

        public List<Cashier.Models.Transaction> TransactionLists;
        public List<Cashier.Models.Report> reports;

        public Report(ApplicationDbContext context)
        {
            CalculatorSoapClient.EndpointConfiguration endpointConfiguration = new CalculatorSoapClient.EndpointConfiguration();
            _calculatorServices = new CalculatorSoapClient(endpointConfiguration);
            _context = context;
            _exportExcel = new ExportExcelServices();
        }

        public async Task OnGetAsync()
        {
            //TransactionLists = await _context.Transactions
            //    .Include(t => t.TransactionDetails)
            //    .Select(t => new Cashier.Models.Transaction
            //    {
            //        ID = t.ID,
            //        TransactionDate = t.TransactionDate,
            //        TotalAmount = t.TotalAmount,
            //        TransactionDetails = t.TransactionDetails.Select(td => new Cashier.Models.TransactionDetail { 
            //            ID = td.ID,
            //            ProductID = td.ProductID,
            //            Quantity = td.Quantity,
            //            UnitPrice = (td.UnitPrice * td.Quantity),
            //            Product = td.Product
            //        }).ToList()
            //    })
            //    .ToListAsync();

            reports = await _context.Reports
                .FromSqlRaw("EXEC Get_TransactionReport")
                .ToListAsync();
        }

        //public IActionResult OnPostExportToPdf()
        //{
        //    var htmlContent = GenerateHtmlContent();
        //    var pdfBytes = GeneratePdfFromHtml(htmlContent);

        //    return File(pdfBytes, "application/pdf", "TransactionReport.pdf");
        //}

        public async Task<IActionResult> OnPostExportToExcel()
        {
            await OnGetAsync();
            DataTable dt = ReportDataTable(reports);
            string sheetName = "Report";
            
            var exportByte = _exportExcel.Export(dt, sheetName);
            return File(exportByte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }




        DataTable ReportDataTable(List<Cashier.Models.Report> reports)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Transaction ID");
            dt.Columns.Add("Transaction Date");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Total Price");

            //foreach(Cashier.Models.Transaction transaction in transactions)
            //{
            //    foreach(TransactionDetail transactionDetail in transaction.TransactionDetails)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr[0] = transaction.ID;
            //        dr[1] = transaction.TransactionDate;
            //        dr[2] = transactionDetail.Product.ProductName;
            //        dr[3] = transactionDetail.Quantity;
            //        dr[4] = transactionDetail.Quantity * transactionDetail.Product.ProductPrice;

            //        dt.Rows.Add(dr);
            //    }
            //}

            foreach (Cashier.Models.Report report in reports)
            {
                DataRow dr = dt.NewRow();
                dr[0] = report.TransactionId;
                dr[1] = report.TransactionDate;
                dr[2] = report.ProductName;
                dr[3] = report.Quantity;
                dr[4] = report.Price;

                dt.Rows.Add(dr);
            }


            return dt;
        }

    }
}
