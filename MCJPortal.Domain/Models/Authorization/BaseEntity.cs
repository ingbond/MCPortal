using System;
using System.ComponentModel.DataAnnotations;

namespace MCJPortal.Domain.Models.Authorization
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}