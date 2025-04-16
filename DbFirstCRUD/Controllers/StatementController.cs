
using ClosedXML.Excel;
using DbFirstCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DbFirstCRUD.Controllers
{
    public class StatementController : Controller
    {
        public IActionResult DownloadStatementExcel()
        {
            // Step 1: Sample Data
            var statement = new DepositStatementData
            {
                DepProfile = new DepProfile
                {
                    AccName = "John Doe",
                    AccNo = "123456789",
                    CustomerID = "C123",
                    RegisteredDate = "2023-01-01",
                    Duration = "12 Months",
                    MaturedDate = "2024-01-01",
                    Interest = "5%",
                    SchemeName = "Regular Saver",
                    ProductName = "Fixed Deposit"
                },
                DepTable = new List<DepTransaction>
                {
                    new DepTransaction { TNO = "T001", TDATE = "2023-01-05", DESCRIPTION = "Deposit", DEBIT = 0, CREDIT = 1000, BALANCE = 1000 },
                    new DepTransaction { TNO = "T002", TDATE = "2023-02-05", DESCRIPTION = "Deposit", DEBIT = 0, CREDIT = 1000, BALANCE = 2000 },
                    new DepTransaction { TNO = "T003", TDATE = "2023-03-05", DESCRIPTION = "Withdraw", DEBIT = 500, CREDIT = 0, BALANCE = 1500 }
                },
                DepFooter = new DepFooter
                {
                    intcapt = 75,
                    inttax = 15
                }
            };

            // Step 2: Create Excel workbook
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Deposit Statement");

            // Step 3: Write Profile Info
            worksheet.Cell(1, 1).Value = "Account Name:";
            worksheet.Cell(1, 2).Value = statement.DepProfile.AccName;

            worksheet.Cell(2, 1).Value = "Account Number:";
            worksheet.Cell(2, 2).Value = statement.DepProfile.AccNo;

            worksheet.Cell(3, 1).Value = "Customer ID:";
            worksheet.Cell(3, 2).Value = statement.DepProfile.CustomerID;

            worksheet.Cell(4, 1).Value = "Registered Date:";
            worksheet.Cell(4, 2).Value = statement.DepProfile.RegisteredDate;

            worksheet.Cell(5, 1).Value = "Duration:";
            worksheet.Cell(5, 2).Value = statement.DepProfile.Duration;

            worksheet.Cell(6, 1).Value = "Matured Date:";
            worksheet.Cell(6, 2).Value = statement.DepProfile.MaturedDate;

            worksheet.Cell(7, 1).Value = "Interest:";
            worksheet.Cell(7, 2).Value = statement.DepProfile.Interest;

            worksheet.Cell(8, 1).Value = "Scheme Name:";
            worksheet.Cell(8, 2).Value = statement.DepProfile.SchemeName;

            worksheet.Cell(9, 1).Value = "Product Name:";
            worksheet.Cell(9, 2).Value = statement.DepProfile.ProductName;

            // Step 4: Table Headers for Transactions
            int currentRow = 11;
            worksheet.Cell(currentRow, 1).Value = "TNo";
            worksheet.Cell(currentRow, 2).Value = "TDate";
            worksheet.Cell(currentRow, 3).Value = "Description";
            worksheet.Cell(currentRow, 4).Value = "Debit";
            worksheet.Cell(currentRow, 5).Value = "Credit";
            worksheet.Cell(currentRow, 6).Value = "Balance";

            // Step 5: Add Transaction Rows
            foreach (var txn in statement.DepTable)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = txn.TNO;
                worksheet.Cell(currentRow, 2).Value = txn.TDATE;
                worksheet.Cell(currentRow, 3).Value = txn.DESCRIPTION;
                worksheet.Cell(currentRow, 4).Value = txn.DEBIT;
                worksheet.Cell(currentRow, 5).Value = txn.CREDIT;
                worksheet.Cell(currentRow, 6).Value = txn.BALANCE;
            }

            // Step 6: Add Footer Info
            currentRow += 2;
            worksheet.Cell(currentRow, 1).Value = "Interest Captured:";
            worksheet.Cell(currentRow, 2).Value = statement.DepFooter.intcapt;

            currentRow++;
            worksheet.Cell(currentRow, 1).Value = "Interest Tax:";
            worksheet.Cell(currentRow, 2).Value = statement.DepFooter.inttax;

            // Step 7: Export to Stream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "DepositStatement.xlsx");
        }
    }
}



