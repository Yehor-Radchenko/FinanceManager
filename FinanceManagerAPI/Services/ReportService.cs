using FinanceManagerAPI.Data;
using FinanceManagerAPI.Data.Operation;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using FinanceManagerAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FinanceManagerAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly FinanceManagerDbContext _context;
        public ReportService(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<ReportViewModel> GetOperationsForPeriod(string inputDate)
        {
            if (!DateTime.TryParseExact(inputDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var checkedDate))
            {
                throw new Exception("Invalid date format. Use format: dd.MM.yyyy");
            }

            var formattedDate = checkedDate.Date;
            var dayOperations = await _context.Operations
                .Where(o => EF.Functions.DateDiffDay(o.DateTime, formattedDate) == 0)
                .Include(o => o.Category)
                .ToListAsync();

            decimal? totalIncome = 0;
            decimal? totalExpense = 0;
            var dayOperationsDto = new List<OperationUpdateDto>();

            foreach (var operation in dayOperations)
            {
                if (operation.Category.Type is OperationType.Income)
                    totalIncome += operation.MoneyAmount;
                if (operation.Category.Type is OperationType.Expense)
                    totalExpense += operation.MoneyAmount;

                dayOperationsDto.Add(new OperationUpdateDto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Description = operation.Description,
                    DateTime = operation.DateTime,
                    MoneyAmount = operation.MoneyAmount,
                    CategoryId = operation.Category.Id
                });
            }
            var report = new ReportViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                operationsForPeriod = dayOperationsDto
            };

            return report;
        }

        public async Task<ReportViewModel> GetOperationsForPeriod(string startDate, string endDate)
        {
            if (!(DateTime.TryParseExact(startDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var checkedStartDate)
                && DateTime.TryParseExact(endDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var checkedEndDate)))
            {
                throw new Exception("Invalid date format. Use format: dd.MM.yyyy");
            }

            var formattedStartDate = checkedStartDate.Date;
            var formattedEndDate = checkedEndDate.Date;

            if (formattedStartDate > formattedEndDate)
                throw new Exception("Start date cannot be later than end date.");

            var periodOperations = await _context.Operations
                .Where(o => o.DateTime.Date >= formattedStartDate && o.DateTime.Date <= formattedEndDate)
                .Include(o => o.Category)
                .ToListAsync();

            decimal? totalIncome = 0;
            decimal? totalExpense = 0;
            var periodOperationsDto = new List<OperationUpdateDto>();

            foreach (var operation in periodOperations)
            {
                if (operation.Category.Type is OperationType.Income)
                    totalIncome += operation.MoneyAmount;
                if (operation.Category.Type is OperationType.Expense)
                    totalExpense += operation.MoneyAmount;

                periodOperationsDto.Add(new OperationUpdateDto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Description = operation.Description,
                    DateTime = operation.DateTime,
                    MoneyAmount = operation.MoneyAmount,
                    CategoryId = operation.Category.Id
                });
            }
            var report = new ReportViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                operationsForPeriod = periodOperationsDto
            };

            return report;
        }
    }
}
