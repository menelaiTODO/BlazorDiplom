using System.ComponentModel.DataAnnotations.Schema;

namespace OLTPDatabaseCore.Models.Base
{
    public abstract class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("creator_name")]
        public string CreatorName { get; set; } = string.Empty;
    }
}
