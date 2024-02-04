using OLTPDatabaseCore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLTPDatabaseCore.Models
{
    public class Order : BaseModel
    {
        [Column("company_id")]
        public int CompanyId { get; set; }

        public Company? Company { get; set; }

        public IEnumerable<OrderGoods>? OrderGoods { get; set; }
    }
}
