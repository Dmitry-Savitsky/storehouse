using EntityFrameworkCore.MySQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;

namespace EntityFrameworkCore.MySQL.Data
{
    public class RepairManagementDbContext : DbContext
    {
        public RepairManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsParam> GoodsParams { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>()
                .HasKey(m => m.IdBuyer);
            modelBuilder.Entity<Goods>()
               .HasKey(m => m.IdGoods);
            modelBuilder.Entity<GoodsParam>()
                .HasKey(m => m.IdGoodsParam);
            modelBuilder.Entity<Manufacturer>()
                .HasKey(m => m.IdManufacturer);
            modelBuilder.Entity<Storage>()
                .HasKey(m => m.IdStorage);
            modelBuilder.Entity<Supplier>()
                .HasKey(m => m.IdSupplier);
            modelBuilder.Entity<Transaction>()
                .HasKey(m => m.IdTransaction);
            modelBuilder.Entity<Warehouse>()
                 .HasKey(m => m.IdWarehouse);
        }
    }
}
