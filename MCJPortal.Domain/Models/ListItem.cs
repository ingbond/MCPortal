using MCJPortal.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCJPortal.Domain.Models
{
    [Table("ListItems")]
    public class ListItem
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public bool ReadOnly { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }


        public ListTypeEnum ListTypeId { get; set; }
        [ForeignKey("ListTypeId")]
        public virtual ListType ListType { get; set; }

        public virtual ICollection<RolePermission> Permissions { get; set; }
    }
}
