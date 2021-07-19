using System;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public class VwTmsShipmentLegsV1
    {
        public string Id { get; set; }
        public string ShipmentId { get; set; }
        public int? LoadId { get; set; }
        public string ShipperReference { get; set; }
        public string ScheduleIntegrationKey { get; set; }
        public string StatusDescription { get; set; }
        public int? StatusDescriptionId { get; set; }
        public int? ShipmentSequence { get; set; }
        public int? WarehouseLoadingSequence { get; set; }
        public int? Distance { get; set; }
        public string DistanceUnit { get; set; }
        public string PickUpStopId { get; set; }
        public int? DropOffStopId { get; set; }
        public string TmsPlanningAbility { get; set; }
        public int? TmsPlanningAbilityId { get; set; }
        public decimal? BillableAllocation { get; set; }
        public string BillableAllocationCurrencyCode { get; set; }
        public decimal? PayableAllocation { get; set; }
        public string PayableAllocationCurrencyCode { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public VwTmsShipmentLegStatusesV1 ShipmentLegStatuses { get; set; }
    }
}
