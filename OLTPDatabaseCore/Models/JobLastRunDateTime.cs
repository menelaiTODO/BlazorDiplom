using OLTPDatabaseCore.Models.Base;

namespace OLTPDatabaseCore.Models
{
    public class JobLastRunDateTime : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public DateTime LastRunDateTime { get; set; }
    }
}
