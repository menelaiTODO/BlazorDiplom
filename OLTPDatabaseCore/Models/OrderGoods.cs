using OLTPDatabaseCore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLTPDatabaseCore.Models
{
    public class OrderGoods : BaseModel
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("goods_id")]
        public int GoodsId { get; set; }

        public Goods? Goods { get; set; }
        
        public Order? Order { get; set; }
    }
}
