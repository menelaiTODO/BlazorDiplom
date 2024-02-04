using OLTPDatabaseCore.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLTPDatabaseCore.Models
{
    public class Company : BaseModel
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("taxpayer_number")]
        public string TaxpayerNumber = string.Empty;
    }
}
