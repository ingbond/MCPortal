using MCJPortal.Domain.Models.Authorization;
using MCJPortal.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCJPortal.Domain.Models
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public AccessEnum Access { get; set; }

        public Guid PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public ListItem Permission { get; set; }

        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
}
