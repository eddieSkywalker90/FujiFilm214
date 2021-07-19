using System;
using System.Collections.Generic;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public partial class VwWmsTmsOrder
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public string OwnerCode { get; set; }
        public string SupplierCode { get; set; }
        public string Shipment { get; set; }
        public string Wave { get; set; }
        public string Comments { get; set; }
        public string ShipFromWarehouseCode { get; set; }
        public int? ShipToWarehouseCode { get; set; }
        public string ShipToName { get; set; }
        public string ShipToStreet1 { get; set; }
        public string ShipToStreet2 { get; set; }
        public string ShipToStreet3 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToPostalCode { get; set; }
        public string FreightPaymentTerms { get; set; }
        public string PoNumber { get; set; }
        public string CarrierService { get; set; }
        public DateTime RequestedShipDate { get; set; }
        public DateTime ScheduledShipDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime CreateStamp { get; set; }
        public DateTime ModStamp { get; set; }
        public int? CloseStamp { get; set; }
        public string ShipFromName { get; set; }
        public string ShipFromStreet1 { get; set; }
        public string ShipFromStreet2 { get; set; }
        public int? ShipFromStreet3 { get; set; }
        public string ShipFromCity { get; set; }
        public string ShipFromState { get; set; }
        public string ShipFromCountry { get; set; }
        public string ShipFromPostalCode { get; set; }
    }
}
