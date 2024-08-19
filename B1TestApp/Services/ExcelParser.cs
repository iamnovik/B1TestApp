using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using B1TestApp.Data.Entity;
using B1TestApp.Repositories.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using CellType = B1TestApp.Utilities.CellType;

namespace B1TestApp.Services;

public class ExcelParser(IUnitOfWork unitOfWork)
{
    public async Task ImportAsync(string filePath)
    {
        using var workbook = new XLWorkbook(filePath);
        var worksheet = workbook.Worksheets.First();
        
        var fileEntity = new Files
        {
            Name = Path.GetFileName(filePath),
        };
        await unitOfWork.Files.AddAsync(fileEntity);
        
        IXLRow previousRow = null;

        var bankName = worksheet.Row(1).Cell(1).GetValue<string>();
        var bank = new Bank()
        {
            Name = bankName,
            File = fileEntity
        };
        await unitOfWork.Banks.AddAsync(bank);
        await unitOfWork.SaveChangesAsync();

        var reportDate = DateTime.Parse(worksheet.Row(6).Cell(1).GetValue<string>());
        foreach (var row in worksheet.RowsUsed().Skip(8))
        {
            var cellType = IsFirstCellInteger(row);
            if (cellType != CellType.StringInfo)
            {
                long accountNumber = 0;
                if (cellType == CellType.ClassTotal)
                {
                    accountNumber = previousRow!.Cell(1).GetValue<long>() / 10;
                }
                else
                {
                    accountNumber = row.Cell(1).GetValue<long>();
                }
                
                var incomingBalanceActive = row.Cell(2).GetValue<decimal>();
                var incomingBalancePassive = row.Cell(3).GetValue<decimal>();
                var turnoverDebit = row.Cell(4).GetValue<decimal>();
                var turnoverCredit = row.Cell(5).GetValue<decimal>();
                var outcomingBalanceActive = row.Cell(6).GetValue<decimal>();
                var outcomingBalancePassive = row.Cell(7).GetValue<decimal>();    

                var accountEntity = new BankAccountData()
                {
                    BankId = bank.Id,
                    BankAccountNumber = accountNumber,
                    ReportYear = reportDate.Year
                };
                var incomingBalance = new IncomingBalance()
                {
                    Assets = incomingBalanceActive,
                    Liabilities = incomingBalancePassive,
                    BankAccountData = accountEntity,
                };
                var turnover = new Turnover()
                {
                    Debit = turnoverDebit,
                    Credit = turnoverCredit,
                    BankAccountData = accountEntity
                };
                var outcomingBalance = new OutcomingBalance()
                {
                    Assets = outcomingBalanceActive,
                    Liabilities = outcomingBalancePassive,
                    BankAccountData = accountEntity,
                };
                
                await unitOfWork.BankAccountsData.AddAsync(accountEntity);
                await unitOfWork.IncomingBalances.AddAsync(incomingBalance);
                await unitOfWork.OutcomingBalances.AddAsync(outcomingBalance);
                await unitOfWork.TurnOvers.AddAsync(turnover);
            }
            previousRow = row;
        }
        await unitOfWork.SaveChangesAsync();
    }
    
    private CellType IsFirstCellInteger(IXLRow row)
    {
        var firstCellValue = row.Cell(1).GetValue<string>();
        
        if (int.TryParse(firstCellValue, out int result))
        {
            return CellType.BankAccount;
        }

        if (firstCellValue.Equals("ПО КЛАССУ"))
        {
            return CellType.ClassTotal;
        }

        return CellType.StringInfo;
    }
}