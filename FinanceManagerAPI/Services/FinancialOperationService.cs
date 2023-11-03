using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
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

        public async Task Create(OperationDto model)
        {
            FinancialOperation operation = new FinancialOperation
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                MoneyAmount = model.MoneyAmount,
                DateTime = model.DateTime,
                CategoryId = model.CategoryId
            };

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if (!await _context.Operations.AnyAsync(c => c.Id == id))
                throw new Exception($"Operation with Id: {id} was not found");

            FinancialOperation operation = new FinancialOperation { Id = id.Value };
            _context.Entry(operation).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationDto>> GetAll()
        {
            List<OperationDto> operations = new List<OperationDto>();
            
            List<FinancialOperation> operationModels = await _context.Operations.ToListAsync();
            foreach (var i in operationModels)
            {
                var operationDto = new OperationDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    MoneyAmount = i.MoneyAmount,
                    DateTime = i.DateTime,
                    CategoryId = i.CategoryId
                };

                operations.Add(operationDto);
            }

            return operations;
        }

        public async Task<OperationDto?> GetById(int? id)
        {
            FinancialOperation? operation = await _context.Operations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (operation is null)
                throw new Exception($"There is no operations with this Id: {id}");

            return new OperationDto 
            { 
                Id = operation.Id, 
                Name = operation.Name,
                Description = operation.Description,
                MoneyAmount = operation.MoneyAmount,
                DateTime = operation.DateTime,
                CategoryId = operation.CategoryId
            };
        }

        public async Task Update(OperationDto expectedEntityValues)
        {
            var existingOperation = await _context.Operations.FirstOrDefaultAsync(g => g.Id == expectedEntityValues.Id);

            if (existingOperation is null)
            {
                throw new ArgumentException("Operation with the specified ID does not exist.");
            }

            _context.Entry(existingOperation).CurrentValues.SetValues(expectedEntityValues);
            await _context.SaveChangesAsync();
        }

        public async Task<ReportDto> GetOperationsForPeriod(string inputDate)
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
            var dayOperationsDto = new List<OperationDto>();

            foreach (var operation in dayOperations)
            {
                if (operation.Category.Type is OperationType.Income)
                    totalIncome += operation.MoneyAmount;
                if (operation.Category.Type is OperationType.Expense)
                    totalExpense += operation.MoneyAmount;

                dayOperationsDto.Add(new OperationDto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Description = operation.Description,
                    DateTime = operation.DateTime,
                    MoneyAmount = operation.MoneyAmount,
                    CategoryId = operation.Category.Id
                });
            }
            var report = new ReportDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                operationsForPeriod = dayOperationsDto
            };

            return report;
        }

        public async Task<ReportDto> GetOperationsForPeriod(string startDate, string endDate)
        {
            if (!(DateTime.TryParseExact(startDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var checkedStartDate)
                && DateTime.TryParseExact(endDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var checkedEndDate)))
            {
                throw new Exception("Invalid date format. Use format: dd.MM.yyyy");
            }

            var formattedStartDate = checkedStartDate.Date;
            var formattedEndDate = checkedEndDate.Date;

            var periodOperations = await _context.Operations
                .Where(o => o.DateTime.Date >= formattedStartDate && o.DateTime.Date <= formattedEndDate)
                .Include(o => o.Category)
                .ToListAsync();

            decimal? totalIncome = 0;
            decimal? totalExpense = 0;
            var periodOperationsDto = new List<OperationDto>();

            foreach (var operation in periodOperations)
            {
                if (operation.Category.Type is OperationType.Income)
                    totalIncome += operation.MoneyAmount;
                if (operation.Category.Type is OperationType.Expense)
                    totalExpense += operation.MoneyAmount;

                periodOperationsDto.Add(new OperationDto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Description = operation.Description,
                    DateTime = operation.DateTime,
                    MoneyAmount = operation.MoneyAmount,
                    CategoryId = operation.Category.Id
                });
            }

            var report = new ReportDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                operationsForPeriod = periodOperationsDto
            };

            return report;
        }

    }
}
