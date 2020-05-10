using MCJPortal.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.ViewModels.ViewModels
{
    public class ListItemViewModel
    {
        public Guid Id { get; set; }
        public ListTypeEnum ListTypeId { get; set; }
        public string Value { get; set; }

        public virtual ICollection<RolePermissionViewModel> Permissions { get; set; }
    }
}
