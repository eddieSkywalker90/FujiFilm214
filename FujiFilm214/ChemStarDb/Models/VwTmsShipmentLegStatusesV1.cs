using System;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public class VwTmsShipmentLegStatusesV1
    {
        public string Id { get; set; }
        public int? LoadId { get; set; }
        public string ShipmentId { get; set; }
        public string ShipmentLegId { get; set; }
        public string StatusAction { get; set; }
        public int? StatusActionId { get; set; }
        public string StatusCode { get; set; }
        public string StatusCodeId { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonCodeId { get; set; }
        public string DataSourceType { get; set; }
        public int? DataSourceTypeId { get; set; }
        public string DataSource { get; set; }
        public int? DataSourceId { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public VwTmsShipmentLegsV1 ShipmentLeg { get; set; }
    }
}
