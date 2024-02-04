using System.ComponentModel.DataAnnotations;

namespace DatawarehouseCore.Models
{
    public class FactSales
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public double Sum { get; set; }
    }
}
