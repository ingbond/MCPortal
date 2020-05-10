using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCJPortal.Domain.Models
{
    public class Documentation
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public byte[] FileDoc { get; set; }
        public bool IsActive { get; set; }
        public string ContentType { get; set; }
        public DateTime DateApproved { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual ProjectLine ProjectLine { get; set; }
    }
}
