using System;

namespace FujiFilm214.FujiFilm.Models
{
    public class Payload
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ShipToAddress { get; set; }
        public DateTime EstimatedShipDate { get; set; }
        public string Sku { get; set; }
    }
}