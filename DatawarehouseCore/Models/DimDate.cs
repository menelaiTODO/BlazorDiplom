using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatawarehouseCore.Models
{
    public class DimDate
    {
        [Key]
        [Column("dim_date_id")]
        public int Id { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("month")]
        public int Month { get; set; }

        [Column("month_name")]
        public string MonthName { get; set; } = string.Empty;

        [Column("day")]
        public int Day { get; set; }
    }
}
