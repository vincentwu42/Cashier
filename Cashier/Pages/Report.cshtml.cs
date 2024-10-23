using Cashier.Data;
using Cashier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System;
using System.Globalization;
using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Cashier.Pages
{
    public class Report : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConverter _converter;

        public List<Cashier.Models.Transaction> TransactionLists;

        public Report(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            TransactionLists = await _context.Transactions
                .Include(t => t.TransactionDetails)
                .Select(t => new Cashier.Models.Transaction
                {
                    ID = t.ID,
                    TransactionDate = t.TransactionDate,
                    TotalAmount = t.TotalAmount
                })
                .ToListAsync();
        }

        public IActionResult OnPostExportToPdf()
        {
            var htmlContent = GenerateHtmlContent();
            var pdfBytes = GeneratePdfFromHtml(htmlContent);

            return File(pdfBytes, "application/pdf", "TransactionReport.pdf");
        }

        private string GenerateHtmlContent()
        {
            var sb = new StringBuilder();

            sb.Append("<html><head><style>");
            sb.Append("table { width: 100%; border-collapse: collapse; }");
            sb.Append("th, td { border: 1px solid black; padding: 5px; text-align: left; }");
            sb.Append("</style></head><body>");
            sb.Append("<h2>Transaction Report</h2>");
            sb.Append("<table><thead><tr><th>ID</th><th>Date</th><th>Price</th></tr></thead><tbody>");

            foreach (var transaction in TransactionLists)
            {
                sb.Append("<tr>");
                sb.Append($"<td>{transaction.ID}</td>");
                sb.Append($"<td>{transaction.TransactionDate.ToShortDateString()}</td>");
                sb.Append($"<td>{transaction.TotalAmount.ToString("C0", new System.Globalization.CultureInfo("id-ID"))}</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody></table>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        private byte[] GeneratePdfFromHtml(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = DinkToPdf.PaperKind.A4
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _converter.Convert(doc);
        }

        public IActionResult OnPostExportToExcel()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Transactions");
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Transaction Date";
                worksheet.Cells[1, 3].Value = "Total Amount";

                for (int i = 0; i < TransactionLists.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = TransactionLists[i].ID;
                    worksheet.Cells[i + 2, 2].Value = TransactionLists[i].TransactionDate.ToShortDateString();
                    worksheet.Cells[i + 2, 3].Value = TransactionLists[i].TotalAmount.ToString("C0", new CultureInfo("id-ID"));
                }

                var excelBytes = package.GetAsByteArray();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransactionReport.xlsx");
            }
        }

    }
}
