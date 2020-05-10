using MCJPortal.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.ViewModels.ViewModels
{
    public class EditPermissionViewModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public AccessEnum? Access { get; set; }
    }
}
