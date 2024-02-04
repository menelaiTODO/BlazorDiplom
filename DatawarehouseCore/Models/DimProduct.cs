using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatawarehouseCore.Models
{
    public class DimProduct
    {
        [Key]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
