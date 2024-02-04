using System.ComponentModel.DataAnnotations;

namespace DatawarehouseCore.Models
{
    public class DimDate
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime OrderDate { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; } = string.Empty;

        public int Day { get; set; }
    }
}
