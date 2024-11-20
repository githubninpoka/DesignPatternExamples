using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCoreAndNorthWindDb.Models; // To use DbContext and so on.

namespace EfCoreAndNorthWindDb.DataAccess
{
    public class NorthwindDb : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFile = ".\\Data\\Northwind.db";
            string path = Path.Combine(
              Environment.CurrentDirectory, databaseFile);
            string connectionString = $"Data Source={path}";
            Console.WriteLine($"Connection: {connectionString}");
            optionsBuilder.UseSqlite(connectionString);
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.EnableDetailedErrors(false);
            //optionsBuilder.LogTo(Console.WriteLine);
#endif
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Example of using Fluent API instead of attributes
            // to limit the length of a category name to 15.
            modelBuilder.Entity<Category>()
              .Property(category => category.CategoryName)
              .IsRequired() // NOT NULL
              .HasMaxLength(15);
            // Some SQLite-specific configuration.
            if (Database.ProviderName?.Contains("Sqlite") ?? false)
            {
                // To "fix" the lack of decimal support in SQLite.
                modelBuilder.Entity<Product>()
                  .Property(product => product.Cost)
                  .HasConversion<double>();
                modelBuilder.Entity<Product>()
                    .Property(product => product.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);
                modelBuilder.Entity<Product>()
                    .Property(product => product.Stock)
                    .HasMaxLength(20);
                
            }
            // A global filter to remove discontinued products.
            modelBuilder.Entity<Product>()
              .HasQueryFilter(p => !p.Discontinued);
        }

        
    }
}