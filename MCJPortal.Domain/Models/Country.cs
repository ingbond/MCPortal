using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] FileDoc { get; set; }
    }
}
