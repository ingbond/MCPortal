using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MCJPortal.Domain.Models.Authorization
{
    public class ApplicationUser : IdentityUser
    {
        public Int64 UserNumber { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTime DateLastAccessed { get; set; }

        public bool IsActive { get; set; }

        public bool IsAllProjectsAllowed { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        //public virtual ApplicationUserRole UserRoles { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}
