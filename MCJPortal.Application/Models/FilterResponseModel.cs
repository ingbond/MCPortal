using System;
using System.Collections.Generic;
using System.Text;

namespace MCJPortal.Application.Models
{
    public class FilterResponseModel<T>
    {
        public IList<T> Data { get; set; }
        /// <summary>
        /// Gets or sets total entity count.
        /// </summary>
        public int Total { get; set; }
    }
}
