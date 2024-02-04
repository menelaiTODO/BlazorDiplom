using Microsoft.EntityFrameworkCore;
using OLTPDatabaseCore.Models;

namespace OLTPDatabaseCore.DatabaseContext
{
    public class OLTPDbContext : DbContext
    {
        public OLTPDbContext(DbContextOptions<OLTPDbContext> options) : base(options) { }

        /// <summary>
        /// Компании
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public DbSet<Goods> Goods { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Многие ко многим. Товары заказа
        /// </summary>
        public DbSet<OrderGoods> OrderGoods{ get; set; }
    }
}
