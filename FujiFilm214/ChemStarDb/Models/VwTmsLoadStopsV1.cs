using System;
using System.Collections.Generic;

#nullable disable

namespace FujiFilm214.ChemStarDb.Models
{
    public partial class VwTmsLoadStopsV1
    {
        public string Id { get; set; }
        public string LoadId { get; set; }
        public int? StopNumber { get; set; }
        public string StopType { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public int? LocationZip { get; set; }
        public string LocationCountry { get; set; }
        public string MergeDate { get; set; }
        public string LocationRef { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string Country { get; set; }
        public string IsPoolPoint { get; set; }
        public DateTime? FileDate { get; set; }
        public int? FileId { get; set; }
    }
}
