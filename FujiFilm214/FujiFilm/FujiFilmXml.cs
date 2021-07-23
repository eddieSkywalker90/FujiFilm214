﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FujiFilm214.ChemStarDb.Models;

namespace FujiFilm214.FujiFilm
{
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
                new XElement("IX", GetIxAttributes(), //Interchange control header
                    new XElement("FG", GetGsAttributes(), //Functional header
                        new XElement("TX", //Start transaction,
                            new XElement("B10"), //Carrier and Ref Id
                            new XElement("L11"), //BOL ref num
                            new XElement("L11"), //Pro ref num
                            new XElement("LXLoop1", //Statuses loop
                                new XElement("LX"), //Status num
                                new XElement("AT7Loop1", //Status details loop
                                    new XElement("AT7"), //Status code and dates
                                    new XElement("MS1"), //Equipment location
                                    new XElement("MS2") //Equipment owner
                                ),
                                new XElement("L11_3") //Some random reference number??
                            )
                        )
                    )
                )
            );
            return xDoc;
            /*
             using var writer = SetupXmlWriter(memoryStream);
            Console.WriteLine("Date: " + DateTime.Now.ToString("yyyyMMdd"));
            Console.WriteLine("Time: " + DateTime.Now.ToString("hhmm"));


            writer.WriteStartElement("FG");
            writer.WriteAttributeString("tag", "GS");
            writer.WriteAttributeString("GS01", "QM");
            writer.WriteAttributeString("GS02", "RCHM");
            writer.WriteAttributeString("GS03", "CFEM");
            writer.WriteAttributeString("GS04", DateTime.Now.ToString("yyyyMMdd"));
            writer.WriteAttributeString("GS05", DateTime.Now.ToString("hhmm"));  
            writer.WriteAttributeString("GS06", "id");
            writer.WriteAttributeString("GS07", "X");
            writer.WriteAttributeString("GS08", "004010");

            writer.WriteStartElement("TX");
            writer.WriteAttributeString("tag", "ST");
            writer.WriteAttributeString("ST01", "214");
            writer.WriteAttributeString("ST02", "0001");

            writer.WriteStartElement("B10");
            writer.WriteAttributeString("tag", "B10");
            writer.WriteAttributeString("B1001", "0073969");


            // var legList = dbContext.VwTmsLoadsV1s
            //     .Take(10)
            //     .Include(load => load.ShipmentLegs).ToList();
            using ChemStarDbContext dbContext = new();
            // var scheduleKey = dbContext.VwTmsLoadsV1s
            //     .Include(load => load.ShipmentLegs)
            //
            //
            //
            // var key = dbContext.VwTmsShipmentLegStatusesV1s
            //     .Where(status => status.Id.Equals(recordId))
                // .Include(l => l.ShipmentLegId.Equals(dbContext.VwTmsShipmentLegStatusesV1s.Include(l => l));
            
            // var ScheduleIntegrationKey = dbContext.VwTmsShipmentLegsV1s
            //     .Where(leg => leg.Id.Equals(dbContext.VwTmsShipmentLegStatusesV1s.First(status => status.Id.Equals(617383252)).Id)).ToList();

            var scheduleIntegrationKey = dbContext.VwTmsShipmentLegStatusesV1s.ToList();
                // .Where(status => status.Id.Equals("617383252"));
                // .Include(status => status.ShipmentLeg);

            // var legRecordEntry = scheduleIntegrationKey.Where(status => status.ShipmentLeg.LoadId.ToString().Equals("104423440"));
            // Console.WriteLine(legRecordEntry.Count());
            //
            // Console.WriteLine("ShipLegID: " + ScheduleIntegrationKey.Where(status => status.ShipmentLeg.LoadId.Equals(125146648)));
            // Console.WriteLine("ShipLegID: " + ScheduleIntegrationKey.First(status => status.ShipmentLeg.LoadId));
            // Console.WriteLine(scheduleIntegrationKey.First().ShipmentLegId);

            // Console.WriteLine("Returning key: Count: " + ScheduleIntegrationKey.Count() + " " + ScheduleIntegrationKey.First().);


                writer.WriteAttributeString("B1002", "Fuji NK to OR_5/6");  // TmsShipmentLegStatus.TmsShipmentLeg.ScheduleIntegrationKey.
            writer.WriteAttributeString("B1003", "RCHM");               // TmsShipmentLegStatus.TmsLoad.CarrierScac.
            writer.WriteEndElement();

            writer.WriteStartElement("L11");
            writer.WriteAttributeString("tag", "L11");
            writer.WriteAttributeString("L1101", "Fuji NK to OR_5/6");  // TmsShipmentLegStatus.TmsShipmentLeg.ScheduleIntegrationKey.
            writer.WriteAttributeString("L1102", "BM");                 // Hardcode - BM = bol number.
            writer.WriteEndElement(); 

            writer.WriteStartElement("L11");
            writer.WriteAttributeString("tag", "L11");
            writer.WriteAttributeString("L1101", "0073969"); // TmsShipmentLegStatus.LoadId.
            writer.WriteAttributeString("L1102", "CN"); // Hardcode - CN = pro.
            writer.WriteEndElement(); 

            writer.WriteStartElement("LXLoop1");
            writer.WriteStartElement("LX");
            writer.WriteAttributeString("tag", "LX");
            writer.WriteAttributeString("LX01", "1");
            writer.WriteEndElement(); 

            writer.WriteStartElement("AT7Loop1");
            writer.WriteStartElement("AT7");
            writer.WriteAttributeString("tag", "AT7");
            writer.WriteAttributeString("AT701", "X1"); // TmsShipmentLegStatus.StatusCode.
            writer.WriteAttributeString("AT702", "NS"); // TmsShipmentLegStatus.ReasonCode.
            writer.WriteAttributeString("AT703", ""); 
            writer.WriteAttributeString("AT704", "");
            writer.WriteAttributeString("AT705", "20210511");  // TmsShipmentLegStatus.StatusDate.
            writer.WriteAttributeString("AT706", "1103");      //TODO:Determine
            writer.WriteAttributeString("AT707", "PT");
            // End AT7 element.
            writer.WriteEndElement(); 

            writer.WriteStartElement("MS1");
            writer.WriteAttributeString("tag", "MS1");
            writer.WriteAttributeString("MS101", "HILLSBORO"); // If pick use shipmentleg.pick else use shipmentleg.drop.
            writer.WriteAttributeString("MS102", "OR");
            writer.WriteAttributeString("MS103", "US");
            writer.WriteEndElement(); 

            writer.WriteStartElement("MS2");
            writer.WriteAttributeString("tag", "MS2");
            writer.WriteAttributeString("MS201", "RCHM"); // TmsShipmentLegStatus.TmsLoad.CarrierScac.
            writer.WriteAttributeString("MS202", "R505");
            writer.WriteEndElement();

            // End AT7Loop1 element.
            writer.WriteEndElement(); 

            writer.WriteStartElement("L11_3");
            writer.WriteAttributeString("tag", "L11");
            writer.WriteAttributeString("L1101", "02");
            writer.WriteAttributeString("L1102", "QN");

            // End LXLoop1 element.
            // End TX element.
            // End FG element.
            // End IX element.
            writer.WriteEndElement(); 
            writer.WriteEndElement(); 
            writer.WriteEndElement(); 
            writer.WriteEndElement(); 
            writer.Flush();
            writer.Close();*/
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
    }
}