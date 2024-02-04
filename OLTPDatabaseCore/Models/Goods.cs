using OLTPDatabaseCore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLTPDatabaseCore.Models
{
    public class Goods : BaseModel
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("sale_price")]
        public double SalePrice { get; set; }
    }
}
