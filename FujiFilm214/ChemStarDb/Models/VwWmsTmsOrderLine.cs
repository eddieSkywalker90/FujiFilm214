using System;
using System.Collections.Generic;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public partial class VwWmsTmsOrderLine
    {
        public string OrderId { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string PoNumber { get; set; }
        public string Comments { get; set; }
        public string WarehouseCode { get; set; }
        public string Sku { get; set; }
        public string Lot { get; set; }
        public decimal Quantity { get; set; }
        public string Uom { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime ModStamp { get; set; }
        public int? CloseStamp { get; set; }
        public int? TmsLoadIds { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string EndUserPartNumber { get; set; }
        public string PmDescription { get; set; }
        public string PmUom1 { get; set; }
        public string PmUom2 { get; set; }
        public string PmUom3 { get; set; }
        public string PmUom4 { get; set; }
        public string PmUom5 { get; set; }
        public string PmUom6 { get; set; }
        public decimal? PmQtyUom1InUom2 { get; set; }
        public decimal? PmQtyUom2InUom3 { get; set; }
        public decimal? PmQtyUom3InUom4 { get; set; }
        public decimal? PmQtyUom4InUom5 { get; set; }
        public decimal? PmQtyUom5InUom6 { get; set; }
        public decimal? PmWeight { get; set; }
        public decimal? PmEmptyWeight { get; set; }
        public string PmEmptyUom1 { get; set; }
        public string PmEmptyUom2 { get; set; }
        public string PmEmptyUom3 { get; set; }
        public decimal? PmEmptyQtyUom1InUom2 { get; set; }
        public decimal? PmEmptyQtyUom2InUom3 { get; set; }
        public string PmHazardClass { get; set; }
        public short? PmIsHazmat { get; set; }
        public short? PmIsTempControlled { get; set; }
    }
}
