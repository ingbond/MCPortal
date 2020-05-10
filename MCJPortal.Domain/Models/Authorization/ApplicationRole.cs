using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCJPortal.Domain.Models.Authorization
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
