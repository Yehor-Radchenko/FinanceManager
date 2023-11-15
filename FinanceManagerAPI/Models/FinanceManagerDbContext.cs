using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Models
{
    public class FinanceManagerDbContext : DbContext
    {
        public FinanceManagerDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            IfTablesEmptyFillTestData();
        }

        public FinanceManagerDbContext()
        {
            Database.EnsureCreated();
            IfTablesEmptyFillTestData();
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

        private void IfTablesEmptyFillTestData()
        {
            if (!this.Categories.Any() && !this.Operations.Any())
            {
                InsertTestDataForCategories();
                InsertTestDataForOperations();
            }
        }

        private void InsertTestDataForOperations()
        {
            var testData = new List<FinancialOperation>
            {
                new FinancialOperation { Name = "Taxi to work", Description = "Taxi to work", CategoryId = 1, DateTime = new DateTime(2023, 10, 31, 8, 1, 59), MoneyAmount = 122},
                new FinancialOperation { Name = "Taxi to restourant", Description = "Taxi to restourant", CategoryId = 1, DateTime = new DateTime(2023, 10, 31, 17, 34, 27), MoneyAmount = 89},
                new FinancialOperation { Name = "Dinner", Description = "Turkey in orange souce", CategoryId = 2, DateTime = new DateTime(2023, 10, 31, 18, 4, 13), MoneyAmount = 352},
                new FinancialOperation { Name = "Taxi to home", Description = "Taxi to home", CategoryId = 1, DateTime = new DateTime(2023, 10, 31, 18, 20, 44), MoneyAmount = 101},
                new FinancialOperation { Name = "Salary", Description = "Salary october", CategoryId = 3, DateTime = new DateTime(2023, 11, 1, 9, 1, 59), MoneyAmount = 25232.34m},
            };

            this.Operations.AddRange(testData);
            this.SaveChanges();
        }

        private void InsertTestDataForCategories()
        {
            var testData = new List<OperationCategory>
            {
                new OperationCategory { Name = "Taxi", Type = OperationType.Expense },
                new OperationCategory { Name = "Restourants", Type = OperationType.Expense },
                new OperationCategory { Name = "Salary", Type = OperationType.Income },
                new OperationCategory { Name = "Products", Type = OperationType.Expense },
                new OperationCategory { Name = "Gym", Type = OperationType.Expense }
            };

            this.Categories.AddRange(testData);
            this.SaveChanges();
        }
    }
}
