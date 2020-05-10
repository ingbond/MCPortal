using Newtonsoft.Json;
using System;

namespace MCJPortal.ViewModels.ViewModels
{
    public class ProjectLineTableViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        
        public string ProjectName { get; set; }
        public int? Number { get; set; }
        public Guid? StatusId { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public string CustomerDescription { get; set; }
        public string Barcode { get; set; }
        public string CustomerNumber { get; set; }
        public string OEMNumber { get; set; }
        public string NickNameIndia { get; set; }
        public string NickNameAustralia { get; set; }
        public Guid? MaterialId { get; set; }
        public double? Weight { get; set; }
        public int? UnitPackagedQty { get; set; }      
        
        public byte[] FileDoc { get; set; }
    }
}
