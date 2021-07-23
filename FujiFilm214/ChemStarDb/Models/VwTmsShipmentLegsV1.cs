using System;
using System.Collections.Generic;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public class VwTmsShipmentLegsV1
    {
        public string Id { get; set; }
        public string ShipmentId { get; set; }
        public string LoadId { get; set; }
        public string ShipperReference { get; set; }
        public string ScheduleIntegrationKey { get; set; }
        public string StatusDescription { get; set; }
        public int? StatusDescriptionId { get; set; }
        public int? ShipmentSequence { get; set; }
        public int? WarehouseLoadingSequence { get; set; }
        public decimal? Distance { get; set; }
        public string DistanceUnit { get; set; }
        public string PickUpStopId { get; set; }
        public string DropOffStopId { get; set; }
        public string TmsPlanningAbility { get; set; }
        public int? TmsPlanningAbilityId { get; set; }
        public decimal? BillableAllocation { get; set; }
        public string BillableAllocationCurrencyCode { get; set; }
        public decimal? PayableAllocation { get; set; }
        public string PayableAllocationCurrencyCode { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public VwTmsLoadsV1 Load { get; set; }
        public VwTmsLoadStopsV1 PickUpStop { get; set; }
        public VwTmsLoadStopsV1 DropOffStop { get; set; }
        public ICollection<VwTmsShipmentLegStatusesV1> ShipmentLegStatuses { get; set; }
    }
}
