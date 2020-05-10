using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.Application.Models
{
    public class FilterProjectLinesModel : FilterModel
    {
        public string ProjectName { get; set; }
        public int? LineNumber { get; set; }
        public string Barcode { get; set; }
        public string Nickname { get; set; }
        public Guid? StatusId { get; set; }
    }
}
