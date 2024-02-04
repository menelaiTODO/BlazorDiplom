using Microsoft.AspNetCore.Identity;

namespace IdentityServerCore.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Последнее время "в сети"
        /// </summary>
        public DateTime? LatestOnline { get; set; }
    }
}
