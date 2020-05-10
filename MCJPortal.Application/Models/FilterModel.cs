using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.Application.Models
{
    public class FilterModel
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string Dir { get; set; }
        public string Field { get; set; }
    }
}
