using FinanceManagerAPI.Data.Operation;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using FinanceManagerAPI.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FinanceManagerAPI.Services
{
    public class FinancialOperationService : IFinancialOperationService
    {
        private readonly FinanceManagerDbContext _context;
        public FinancialOperationService(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(OperationCreateDto model)
        {
            if (model.MoneyAmount <= 0)
                throw new ArgumentException("Money amount cannot be less than zero.");

            try
            {
                FinancialOperation operation = new FinancialOperation
                {
                    Name = model.Name,
                    Description = model.Description,
                    MoneyAmount = model.MoneyAmount,
                    DateTime = model.DateTime,
                    CategoryId = model.CategoryId
                };
                _context.Operations.Add(operation);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int? id)
        {
            if (!await _context.Operations.AnyAsync(c => c.Id == id))
                throw new Exception($"Operation with Id: {id} was not found");
            try
            {
                FinancialOperation operation = new FinancialOperation { Id = id.Value };
                _context.Entry(operation).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<OperationViewModel>> GetAll()
        {
            List<FinancialOperation> operationModels = await _context.Operations.ToListAsync();
            List<OperationViewModel> operations = await _context.Operations
                .Select( x => new OperationViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MoneyAmount = x.MoneyAmount,
                    DateTime = x.DateTime,
                    CategoryName =  _context.Categories.FirstOrDefault(c => c.Id == x.CategoryId).Name
                }) 
                .ToListAsync();

            return operations;
        }

        public async Task<OperationViewModel?> GetById(int? id)
        {
            FinancialOperation? operation = await _context.Operations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (operation is null)
                throw new Exception($"There is no operations with this Id: {id}");

            return new OperationViewModel
            { 
                Id = operation.Id, 
                Name = operation.Name,
                Description = operation.Description,
                MoneyAmount = operation.MoneyAmount,
                DateTime = operation.DateTime,
                CategoryName = _context.Categories.FirstOrDefault(c => c.Id == id).Name
            };
        }

        public async Task<bool> Update(OperationUpdateDto expectedEntityValues)
        {
            if (expectedEntityValues.MoneyAmount <= 0)
                throw new ArgumentException("Money amount cannot be less than zero.");

            var existingOperation = await _context.Operations.FirstOrDefaultAsync(g => g.Id == expectedEntityValues.Id);

            if (existingOperation is null)
                throw new ArgumentException("Operation with the specified ID does not exist.");
            try
            {
                _context.Entry(existingOperation).CurrentValues.SetValues(expectedEntityValues);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
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
            var dayOperationViewModels = new List<OperationViewModel>();

            foreach (var operation in dayOperations)
            {
                if (operation.Category.Type is OperationType.Income)
                    totalIncome += operation.MoneyAmount;
                if (operation.Category.Type is OperationType.Expense)
                    totalExpense += operation.MoneyAmount;

                dayOperationViewModels.Add(new OperationViewModel
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Description = operation.Description,
                    DateTime = operation.DateTime,
                    MoneyAmount = operation.MoneyAmount,
                    CategoryName = _context.Operations.FirstOrDefault(c => c.Id == operation.Category.Id).Name
                });
            }
            var report = new ReportViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                operationsForPeriod = dayOperationViewModels
            };

            return report;
        }
    }
}
