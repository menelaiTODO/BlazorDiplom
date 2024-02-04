using DatawarehouseCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DatawarehouseCore.DatabaseContext
{
    public class DWHDbContext : DbContext
    {
        public DWHDbContext(DbContextOptions<DWHDbContext> options) : base(options) { }

        /// <summary>
        /// Компании
        /// </summary>
        public DbSet<DimDate> DimDates { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public DbSet<DimOrder> DimOrders { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<DimProduct> DimProducts { get; set; }

        /// <summary>
        /// Многие ко многим. Товары заказа
        /// </summary>
        public DbSet<FactSales> FactSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
