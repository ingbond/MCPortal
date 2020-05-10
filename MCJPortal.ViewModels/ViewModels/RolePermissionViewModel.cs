using MCJPortal.Domain.Models.Enums;
using System;

namespace MCJPortal.ViewModels.ViewModels
{
    public class RolePermissionViewModel
    {
        public string RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public AccessEnum Access { get; set; }
    }
}
