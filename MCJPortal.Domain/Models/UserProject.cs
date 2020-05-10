using MCJPortal.Domain.Models.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCJPortal.Domain.Models
{
    [Table("UserProjects")]
    public class UserProject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get;set;}

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
