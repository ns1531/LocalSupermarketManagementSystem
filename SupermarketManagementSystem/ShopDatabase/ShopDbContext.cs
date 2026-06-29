using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using SupermarketManagementSystem.ShopData;

namespace SupermarketManagementSystem.ShopDatabase
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Stock> Stock { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<SaleItem> SaleItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=LocalSupermarketManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(category => category.CategoryId);
            modelBuilder.Entity<Supplier>().HasKey(supplier => supplier.SupplierId);
            modelBuilder.Entity<Product>().HasKey(product => product.ProductId);
            modelBuilder.Entity<Stock>().HasKey(stock => stock.ProductId);
            modelBuilder.Entity<Sale>().HasKey(sale => sale.SaleId);
            modelBuilder.Entity<SaleItem>().HasKey(saleItem => saleItem.SaleItemId);

            modelBuilder.Entity<Product>().Ignore(product => product.QuantityInStock);
            modelBuilder.Entity<Product>().Ignore(product => product.LowStockThreshold);
            modelBuilder.Entity<Product>().Ignore(product => product.StockStatus);

            modelBuilder.Entity<Sale>().Ignore(sale => sale.Item);
        }

    }
}