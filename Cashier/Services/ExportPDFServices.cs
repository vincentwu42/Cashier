using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Data;

namespace Cashier.Services
{
    public class ExportPDFServices
    {
        //public byte[] ExportToPDF(DataTable dataTable)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        PdfReader pdfReader = new PdfReader(ms);
        //        PdfDocument pdfDocument = new PdfDocument(pdfReader);
        //        Document document = new Document(pdfDocument);
        //        PdfWriter.GetInstance(document, ms);
        //        document.Open();

        //        // Add content to PDF
        //        document.Add(new Paragraph("Transaction Report"));
        //        PdfPTable table = new PdfPTable(3); // 3 columns

        //        // Add headers
        //        table.AddCell("ID");
        //        table.AddCell("Date");
        //        table.AddCell("Total Amount");

        //        // Add rows
        //        foreach (var transaction in transactions)
        //        {
        //            table.AddCell(transaction.ID.ToString());
        //            table.AddCell(transaction.TransactionDate.ToString());
        //            table.AddCell(transaction.TotalAmount.ToString("C"));
        //        }

        //        document.Add(table);
        //        document.Close();

        //        return ms.ToArray(); // Return PDF as byte array
        //    }
        //}
    }
}
