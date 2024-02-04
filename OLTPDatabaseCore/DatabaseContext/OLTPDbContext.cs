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
    }
}
