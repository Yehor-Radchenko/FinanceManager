using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                operations.Add(new OperationDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    MoneyAmount = i.MoneyAmount,
                    DateTime = i.DateTime,
                    CategoryId = i.CategoryId
                });
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
    }
}
