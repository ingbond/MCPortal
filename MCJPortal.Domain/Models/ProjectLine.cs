using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MCJPortal.Domain.Models
{
    [Table("ProjectLines")]
    public class ProjectLine
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string CustomerDescription { get; set; }
        public string Barcode { get; set; }
        public string OldSystemPartNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string OEMNumber { get; set; }
        public Guid? GroupId { get; set; }
        public string NickNameIndia { get; set; }
        public string NickNameAustralia{ get; set; }
        public int? EconomManufacturerQty { get; set; }
        public int? InventoredItem { get; set; }
        public int? MaxStockQty { get; set; }
        public int? MinStockQty { get; set; }
        public string ProductionLocation { get; set; }
        public decimal? RawCost { get; set; }
        public decimal? ProcessCost { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? RecoringToolingCost { get; set; }
        public int? ManualOrderQty { get; set; }
        public int? TotalOrderQty { get; set; }
        public int? LiveOrder { get; set; }
        public DateTime? FinalCompletionDate { get; set; }
        public Guid? TypeOfProductionId { get; set; }
        public Guid? DispatchModeId { get; set; }
        
        public double? Weight { get; set; }
        public int? UnitPackagedQty { get; set; }
        public decimal? PriceOld { get; set; }
        public decimal PriceCurrent { get; set; }
        public string RackLocationAU { get; set; }
        public string SalesOrderNumber { get; set; }
        public string ProductionTeamInstructions { get; set; }
        public string MJInstructions { get; set; }
        public string ShippingTrackingNumber { get; set; }
        public string PatternLocationAU { get; set; }
        public string PatternLocationIND { get; set; }
        public int? PartDelivery { get; set; }
        public int? BackorderQty { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid StatusId { get; set; }
        public virtual ListItem Status { get; set; }
        public Guid CurrencyId { get; set; }
        public virtual ListItem Currency { get; set; }

        public virtual Documentation Document { get; set; }

        public Guid? MaterialId { get; set; }
        public virtual ListItem Material { get; set; }
    }
}
