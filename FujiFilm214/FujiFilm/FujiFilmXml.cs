﻿using System;
using System.IO;
using System.Text;
using System.Xml;

namespace FujiFilm214.FujiFilm
{
    public class FujiFilmXml
    {
        public void Build(MemoryStream memoryStream)
        {
            Console.WriteLine("Building XML.. \n");
            using var writer = SetupXmlWriter(memoryStream);

            writer.WriteStartElement("IX");
            writer.WriteAttributeString("tag", "ISA");
            writer.WriteAttributeString("ISA01", "00");
            writer.WriteAttributeString("ISA02", "          ");
            writer.WriteAttributeString("ISA03", "00");
            writer.WriteAttributeString("ISA04", "          ");
            writer.WriteAttributeString("ISA05", "02");
            writer.WriteAttributeString("ISA06", "RCHM           ");
            writer.WriteAttributeString("ISA07", "01");
            writer.WriteAttributeString("ISA08", "067888030      ");
            writer.WriteAttributeString("ISA09", "210512");
            writer.WriteAttributeString("ISA10", "1613");
            writer.WriteAttributeString("ISA11", "U");
            writer.WriteAttributeString("ISA12", "00401");
            writer.WriteAttributeString("ISA13", "0000000id");
            writer.WriteAttributeString("ISA14", "0");
            writer.WriteAttributeString("ISA15", "p");
            writer.WriteAttributeString("ISA16", ":~");

            writer.WriteStartElement("FG");
            writer.WriteAttributeString("tag", "GS");
            writer.WriteAttributeString("GS01", "QM");
            writer.WriteAttributeString("GS02", "RCHM");
            writer.WriteAttributeString("GS03", "CFEM");
            writer.WriteAttributeString("GS04", "20210512"); //TODO: Calc date.
            writer.WriteAttributeString("GS05", "1613"); //TODO: Calc time.
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
            writer.Close();
        }

        /// <summary>
        ///     Method to configure settings for the XmlWriter used to build the Xml data stream.
        /// </summary>
        /// <param name="memoryStream">Memory stream object containing an XML formatted data entry set(a record).
        /// Teated as the 'TPayload' in the Janky.IntegrationManager classes.</param>
        /// <returns>Memory stream object that will be sent to Janky.IntegrationManager for processing.</returns>
        public XmlWriter SetupXmlWriter(MemoryStream memoryStream)
        {
            XmlWriterSettings settings = new();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);
            settings.ConformanceLevel = ConformanceLevel.Document;
            var writer = XmlWriter.Create(memoryStream, settings);
        
            return writer;
        }
    }
}