
using MCJPortal.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCJPortal.Domain.Models
{
    [Table("ListTypes")]
    public class ListType
    {
        [Key]
        public ListTypeEnum Id { get; set; }
        public string Name { get; set; }
        public bool ReadOnly { get; set; }
        public bool IsActive { get; set; }
        public bool? IsTree { get; set; }
    }
}
