using System;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public class VwTmsLoadsV1
    {
        public string Id { get; set; }
        public string Division { get; set; }
        public string LoadGroup { get; set; }
        public string IsCanceled { get; set; }
        public string TourId { get; set; }
        public string VehicleNumber { get; set; }
        public string ContainerNumber { get; set; }
        public string TrailerNumber { get; set; }
        public string DriverName { get; set; }
        public string CarrierScac { get; set; }
        public string CarrierName { get; set; }
        public string DirectionCategory { get; set; }
        public string Mode { get; set; }
        public string ServiceLevel { get; set; }
        public string LastExecutedEvent { get; set; }
        public string LastExecutedEventId { get; set; }
        public string Equipment { get; set; }
        public string EquipmentCode { get; set; }
        public string EquipmentType { get; set; }
        public decimal? Distance { get; set; }
        public string DistanceUom { get; set; }
        public decimal? Volume { get; set; }
        public string VolumeUom { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUom { get; set; }
        public decimal? PieceQuantity { get; set; }
        public decimal? PalletQuantity { get; set; }
        public decimal? LinearSpace { get; set; }
        public string LinearSpaceUom { get; set; }
        public decimal? Density { get; set; }
        public string DensityUom { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public VwTmsShipmentLegsV1 ShipmentLegs { get; set; }
        public VwTmsLoadStopsV1 ShipmentLoadStops { get; set; }
    }
}
