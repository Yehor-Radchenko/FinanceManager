using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Models
{
    public class FinanceManagerDbContext : DbContext
    {
        public FinanceManagerDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public FinanceManagerDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<OperationCategory> Categories { get; set; }
        public DbSet<FinancialOperation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialOperation>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<FinancialOperation>()
                .HasOne(o => o.Category)
                .WithMany(c => c.FinancialOperations)
                .HasForeignKey(o => o.CategoryId);

            modelBuilder.Entity<OperationCategory>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<OperationCategory>()
                .Property(o => o.Type)
                .HasConversion<int>();

            modelBuilder.Entity<OperationCategory>().ToTable("Category");
            modelBuilder.Entity<FinancialOperation>().ToTable("Operation");
        }
    }
}
