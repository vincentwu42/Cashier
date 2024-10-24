using ClosedXML.Excel;
using System.Data;

namespace Cashier.Services
{
    public class ExportExcelServices
    {
        public byte[] Export(DataTable dataTable, string sheetName)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                }

                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        string value = dataTable.Rows[row][col].ToString();
                        worksheet.Cell(row + 2, col + 1).Value = value;
                    }
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
