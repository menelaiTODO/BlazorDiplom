using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatawarehouseCore.Models
{
    public class FactSales
    {
        [Key]
        [Column("fact_sale_id")]
        public int FactSalesId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("sum")]
        public double Sum { get; set; }
    }
}
