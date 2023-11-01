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

        public async Task Create(FinancialOperation model)
        {
            _context.Operations.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            FinancialOperation operation = new FinancialOperation { Id = id.Value };
            _context.Entry(operation).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinancialOperation>> GetAll()
        {
            return await _context.Operations.Include(s => s.Category).ToListAsync();
        }

        public async Task<FinancialOperation?> GetById(int? id)
        {
            return await _context.Operations.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Update(FinancialOperation expectedEntityValues)
        {
            var existingOperation = await _context.Operations.FirstOrDefaultAsync(g => g.Id == expectedEntityValues.Id);

            if (existingOperation is null)
            {
                throw new ArgumentException("Operation with the specified ID does not exist.");
            }

            _context.Operations.Update(expectedEntityValues);
            await _context.SaveChangesAsync();
        }
    }
}
