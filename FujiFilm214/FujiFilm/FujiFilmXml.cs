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
                new XElement("Interchange",
                    new XElement("Meta", GetIxAttributes()),                                                            // Interchange control header.
                    new XElement("FunctionalGroup",                                                                     // Functional header.
                        new XElement("Meta", GetGsAttributes()),
                        new XElement("TransactionSet",                                                                  // Start transaction.
                            new XElement("TX-00401-214", new XAttribute("type", "TransactionSet"),
                                new XElement("Meta", GetStAttributes()),
                                new XElement("B10", GetB10Attributes(), new XAttribute("type", "Segment")),             // Carrier and Ref Id.
                                new XElement("L11", GetL11Attributes(), new XAttribute("type", "Segment")),             // BOL ref num.
                                new XElement("L11", GetSecondL11Attributes(), new XAttribute("type", "Segment")),       // Pro ref num.
                                new XElement("LXLoop1", new XAttribute("type", "Loop"),                                 // Statuses loop.
                                    new XElement("LX", GetLxAttributes(), new XAttribute("type", "Segment")),           // Status num.
                                    new XElement("AT7Loop1", new XAttribute("type", "Loop"),                            // Status details loop.
                                        new XElement("AT7", GetAt7Attributes(), new XAttribute("type", "Segment")),     // Status code and dates.
                                        new XElement("MS1", GetMs1Attributes(), new XAttribute("type", "Segment")),     // Equipment location.
                                        new XElement("MS2", GetMs2Attributes(), new XAttribute("type", "Segment"))      // Equipment owner.
                                    ),
                                    new XElement("L11", GetL11_3Attributes(), new XAttribute("type", "Segment"))      // Some random reference number??
                                )
                            )
                        )
                    )
                )
            );
            return xDoc;
        }

        private List<XElement> GetStAttributes()
        {
            return new()
            {
                new XElement("ST01", "214"),
                new XElement("ST02", "0001")
            };
        }

        private List<XElement> GetIxAttributes()
        {
            var now = DateTime.Now;
            return new List<XElement>
            {
                new("ISA01", "00"),
                new("ISA02"),
                new("ISA03", "00"),
                new("ISA04"),
                new("ISA05", "02"),
                new("ISA06", "RCHM "),
                new("ISA07", "01"),
                new("ISA08", "067888030 "),
                new("ISA09", now.ToString("yyMMdd")),
                new("ISA10", now.ToString("hhmm")),
                new("ISA11", "U"),
                new("ISA12", "00401"),
                new("ISA13", "0000000id"),
                new("ISA14", "0"),
                new("ISA15", "p"),
                new("ISA16", ":")
            };
        }

        private List<XElement> GetGsAttributes()
        {
            var now = DateTime.Now;
            return new List<XElement>
            {
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

        private List<XElement> GetB10Attributes()
        {
            return new()
            {
                new XElement("B1001", ""), //Waiting on doc
                new XElement("B1002", ShipmentLegStatus.ShipmentLeg.ScheduleIntegrationKey),
                new XElement("B1003", ShipmentLegStatus.ShipmentLeg.Load.CarrierScac)
            };
        }

        private List<XElement> GetL11Attributes()
        {
            return new()
            {
                new XElement("L1101", ShipmentLegStatus.ShipmentLeg.ScheduleIntegrationKey),
                new XElement("L1102", "BM"),
                new XElement("L1103", "")
            };
        }

        private List<XElement> GetSecondL11Attributes()
        {
            return new()
            {
                new XElement("L1101", ShipmentLegStatus.LoadId),
                new XElement("L1102", "CN")
            };
        }

        private List<XElement> GetLxAttributes()
        {
            return new()
            {
                new XElement("LX01", "1")
            };
        }

        private List<XElement> GetAt7Attributes()
        {
            return new()
            {
                new XElement("AT701", ShipmentLegStatus.StatusCode),
                new XElement("AT702", ShipmentLegStatus.ReasonCode),
                new XElement("AT703", ""),
                new XElement("AT704", ""),
                new XElement("AT705", ShipmentLegStatus.StatusDate),
                new XElement("AT706", DateTime.Now.ToString("hhmm")),
                new XElement("AT707", "PT") //Waiting on doc
            };
        }

        private List<XElement> GetMs1Attributes()
        {
            if (ShipmentLegStatus.StatusAction.ToUpper().Contains("PICK"))
            {
                return new List<XElement>
                {
                    new("MS101", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationCity)),
                    new("MS102", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationState)),
                    new("MS103", "") //TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.PickUpStop.LocationCountry))
                };
            }

            return new List<XElement>
            {
                new("MS101", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationCity)""),
                new("MS102", ""),//TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationState)),
                new("MS103", "") //TemporaryNullChecker(ShipmentLegStatus.ShipmentLeg.DropOffStop.LocationCountry))
            };
        }

        private List<XElement> GetMs2Attributes()
        {
            return new()
            {
                // new("tag", "MS2"), //Waiting on doc
                new XElement("MS201", ShipmentLegStatus.ShipmentLeg.Load.CarrierScac),
                new XElement("MS202", "")
            };
        }

        private List<XElement> GetL11_3Attributes()
        {
            return new()
            {
                // new("tag", "L11_3"), //Waiting on doc
                new XElement("L1101", "02"),
                new XElement("L1102", "QN")
            };
        }
    }
}