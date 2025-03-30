using FuzzyDataDbCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FuzzyDataDbCore.DatabaseContext
{
    public class FuzzyDataDbContext : DbContext
    {
        public FuzzyDataDbContext(DbContextOptions<FuzzyDataDbContext> options) : base(options) { }

        /// <summary>
        /// Точки
        /// </summary>
        public DbSet<Point> Points { get; set; }

        /// <summary>
        /// Нечеткие одиночные лингв. переменные
        /// </summary>
        public DbSet<CustomLinguisticVariable> CustomLinguisticVariables { get; set; }

        /// <summary>
        /// Нечеткие составные лингв. переменные
        /// </summary>
        public DbSet<CustomMultiplyLinguisticVariable> CustomMultiplyLinguisticVariables { get; set; }
    }
}
