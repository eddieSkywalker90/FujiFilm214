using System.Collections.Generic;

namespace FujiFilm214.FujiFilm.Models
{
    public class Payload
    {
        public List<string> InterchangeControlHeader;
        public List<string> FunctionalGroupHeader;
        public List<string> TransactionSetHeader;
        public List<string> CarrierShipmentStatus;
        public List<string> BusinessInteractionReferenceNumber;
        public List<string> AssignedNumber;
        public List<string> ShipmentStatusDetails;
        public List<string> EquipmentShipmentOrRealPropertyLocation;
        public List<string> EquipmentContainerOwnerAndType;
        public List<string> BusinessInstructionsReferenceNumber;
        public List<string> TransationSetTrailer;
        public List<string> FunctionGroupTrailer;
        public List<string> InterchangeControlTrailer;
    }
}