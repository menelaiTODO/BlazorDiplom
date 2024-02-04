using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatawarehouseCore.Models
{
    public class DimOrder
    {
        [Key]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("account_name")]
        public string AccountName { get; set; } = string.Empty;
    }
}
