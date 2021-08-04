using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FujiFilm214.ChemStarDb.Models;

namespace FujiFilm214.FujiFilm
{
    /// <summary>
    ///     Class used to aid in the creation of the XML Payload object.
    /// </summary>
    public class FujiFilmXml
    {
        public VwTmsShipmentLegStatusesV1 ShipmentLegStatus;

        public FujiFilmXml(VwTmsShipmentLegStatusesV1 shipmentLegStatus)
        {
            ShipmentLegStatus = shipmentLegStatus;
        }

        public XDocument Build()
        {
            var xDoc = new XDocument(
                new XElement("IX", GetIxAttributes(),                       // Interchange control header.
                    new XElement("FG", GetGsAttributes(),                   // Functional header.
                        new XElement("TX",                                  // Start transaction.
                            new XElement("B10", GetB10Attributes()),        // Carrier and Ref Id.
                            new XElement("L11", GetL11Attributes()),        // BOL ref num.
                            new XElement("L11", GetSecondL11Attributes()),  // Pro ref num.
                            new XElement("LXLoop1",                         // Statuses loop.
                                new XElement("LX", GetLxAttributes()),      // Status num.
                                new XElement("AT7Loop1",                    // Status details loop.
                                    new XElement("AT7", GetAt7Attributes()),// Status code and dates.
                                    new XElement("MS1", GetMs1Attributes()),// Equipment location.
                                    new XElement("MS2", GetMs2Attributes()) // Equipment owner.
                                ),
                                new XElement("L11_3", GetL11_3Attributes()) // Some random reference number??
                            )
                        )
                    )
                )
            );
            return xDoc;
        }

        private List<XAttribute> GetIxAttributes()
        {
            return new()
            {
                new XAttribute("tag", "ISA"),
                new XAttribute("ISA01", "00"),
                new XAttribute("ISA02", "          "),
                new XAttribute("ISA03", "00"),
                new XAttribute("ISA04", "          "),
                new XAttribute("ISA05", "02"),
                new XAttribute("ISA06", "RCHM           "),
                new XAttribute("ISA07", "01"),
                new XAttribute("ISA08", "067888030      "),
                new XAttribute("ISA09", "210512"),
                new XAttribute("ISA10", "1613"),
                new XAttribute("ISA11", "U"),
                new XAttribute("ISA12", "00401"),
                new XAttribute("ISA13", "0000000id"),
                new XAttribute("ISA14", "0"),
                new XAttribute("ISA15", "p"),
                new XAttribute("ISA16", ":~")
            };
        }

        private List<XAttribute> GetGsAttributes()
        {
            var now = DateTime.Now;
            return new List<XAttribute>
            {
                new("tag", "GS"),
                new("GS01", "QM"),
                new("GS02", "RCHM"),
                new("GS03", "CFEM"),
                new("GS04", now.ToString("yyyyMMdd")),
                new("GS05", now.ToString("hhmm")),
                new("GS06", "id"),
                new("GS07", "X"),
                new("GS08", "004010")
            };
        }

        private List<XAttribute> GetB10Attributes()
        {
            return new()
            {
                new XAttribute("tag", "B10"),
                new XAttribute("B1001", ""), //Waiting on doc
                new XAttribute("B1002", ShipmentLegStatus.ShipmentLeg.ScheduleIntegrationKey),
                new XAttribute("B1003", ShipmentLegStatus.ShipmentLeg.Load.CarrierScac)
            };
        }

        private List<XAttribute> GetL11Attributes()
        {
            return new()
            {
                new XAttribute("tag", "L11"),
                new XAttribute("L1101", ShipmentLegStatus.ShipmentLeg.ScheduleIntegrationKey),
                new XAttribute("L1102", "BM"),
                new XAttribute("L1103", "")
            };
        }

        private List<XAttribute> GetSecondL11Attributes()
        {
            return new()
            {
                new XAttribute("tag", "L11"),
                new XAttribute("L1101", ShipmentLegStatus.LoadId),
                new XAttribute("L1102", "CN")
            };
        }

        private List<XAttribute> GetLxAttributes()
        {
            return new()
            {
                new XAttribute("tag", "LX"),
                new XAttribute("LX01", "1")
            };
        }

        private List<XAttribute> GetAt7Attributes()
        {
            return new()
            {
                new XAttribute("tag", "AT7"),
                new XAttribute("AT701", ShipmentLegStatus.StatusCode),
                new XAttribute("AT702", ShipmentLegStatus.ReasonCode),
                new XAttribute("AT703", ""),
                new XAttribute("AT704", ""),
                new XAttribute("AT705", ShipmentLegStatus.StatusDate),
                new XAttribute("AT706", DateTime.Now.ToString("hhmm")),
                new XAttribute("AT707", "PT") //Waiting on doc
            };
        }

        private List<XAttribute> GetMs1Attributes()
        {
            if (ShipmentLegStatus.StatusAction.ToUpper().Contains("PICK"))
            {
                return new List<XAttribute>
                {
                    new("tag", "MS1"), 
                    new("MS101", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationCity)),
                    new("MS102", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationState)),
                    new("MS103", "") //TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationCountry))
                };
            }

            return new List<XAttribute>
            {
                new("tag", "MS1"),
                new("MS101", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationCity)""),
                new("MS102", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationState)),
                new("MS103", "") //TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationCountry))
            };
        }

        //TEMP METHOD ATM USED TO RETURN EMPTY STR IF NULL. WASN'T WORKING FOR ME SO I JUST FORCED EMPTY STR ABOVE, FOR NOW.
        private static object TemporaryNullChecker(string argument)
        {
            if (string.IsNullOrEmpty(argument))
                return "";
            return argument;
        }

        private List<XAttribute> GetMs2Attributes()
        {
            return new()
            {
                new XAttribute("tag", "MS2"), //Waiting on doc
                new XAttribute("MS201", ShipmentLegStatus.ShipmentLeg.Load.CarrierScac),
                new XAttribute("MS202", "")
            };
        }

        private List<XAttribute> GetL11_3Attributes()
        {
            return new()
            {
                new XAttribute("tag", "L11_3"), //Waiting on doc
                new XAttribute("L1101", "02"),
                new XAttribute("L1102", "QN")
            };
        }
    }
}