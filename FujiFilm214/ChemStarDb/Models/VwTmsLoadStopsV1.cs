using System;
using System.Collections.Generic;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public class VwTmsLoadStopsV1
    {
        public string Id { get; set; }
        public string LoadId { get; set; }
        public int? StopNumber { get; set; }
        public string StopType { get; set; }
        public string LocationId { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public DateTime? EstimatedDepartureDate { get; set; }
        public DateTime? ActualArrivalDate { get; set; }
        public DateTime? ActualDepartureDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public int? LocationZip { get; set; }
        public string LocationCountry { get; set; }

        public VwTmsLoadsV1 Load { get; set; }
        public ICollection<VwTmsShipmentLegsV1> DropOffShipmentLegs { get; set; }
        public ICollection<VwTmsShipmentLegsV1> PickUpShipmentLegs { get; set; }
    }
}
