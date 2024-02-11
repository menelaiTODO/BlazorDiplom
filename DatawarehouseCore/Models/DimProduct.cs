using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatawarehouseCore.Models
{
    public class DimProduct
    {
        [Key]
        [Column("dim_product_id")]
        public int Id { get; set; } 

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
