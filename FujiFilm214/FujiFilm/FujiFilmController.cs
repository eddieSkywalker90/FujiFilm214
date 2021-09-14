using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using FujiFilm214.ChemStarDb;
using FujiFilm214.ChemStarDb.Models;
using Janky.Utilities.Api;
using Janky.Utilities.Ftp;
using JankyIntegrationManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FujiFilm214.FujiFilm
{
    /// <summary>
    ///     This class implements the DeltaIntegrationManager and therefore the IntegrationManager
    ///     from the JankyIntegrationManager project. This class main intention is to fill out the needed
    ///     methods the meta project requires in order to build the change status payloads and ship off to
    ///     the data to an expected destination.
    /// </summary>
    public class FujiFilmController : DeltaCheckIntegrationManager<XDocument>
    {
        public FujiFilmController(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
        }

        /// <summary>
        ///     Call associated SQL View to return list of potentially new/updated records.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns>A List of record id's.</returns>
        protected override List<string> IdentifyPotentiallyChangedRecordIds(DateTime? startTime)
        {
            try
            {
                // Access ChemStar DB to pull data.
                using ChemStarDbContext dbContext = new();
                List<VwTmsShipmentLegStatusesV1> changedStatuses;

                // Dev-only environment returns limited return with no specific customer to run faster for debugging.
                if (Configuration.Environment == "Development")
                    changedStatuses = dbContext.VwTmsShipmentLegStatusesV1s.Take(3).ToList();
                else // Production environment returns all.
                    changedStatuses = dbContext.VwTmsShipmentLegStatusesV1s
                        .Where(status => status.ShipmentLeg.Customer == "FujiFilm" && status.UpdatedAt > startTime)
                        .ToList();

                // Grab each entries ID as the RecordId.
                List<string> newOrUpdatedRecordsIds = new();
                foreach (var status in changedStatuses)
                {
                    newOrUpdatedRecordsIds.Add(status.Id);
                }
                Log.Information($"Returning list of {changedStatuses.Count} record entries..");

                return newOrUpdatedRecordsIds;
            }
            catch (Exception e)
            {
                Log.Debug(e, "Access to ChemStar database to retrieve status changes failed.");
                throw;
            }
        }

        /// <summary>
        ///     Using the collection of returned record id's generated from IdentifyPotentiallyChangedRecordIds(),
        ///     query the SQLDW database to create a payload from joining matching recordID's in associated tables.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        protected override XDocument GetRecordPayload(string recordId)
        {
            Log.Debug($"Building XML for record #{recordId}..");

            try
            {
                using ChemStarDbContext dbContext = new();

                var tmsStatuses = dbContext.VwTmsShipmentLegStatusesV1s
                    .Include(status => status.ShipmentLeg)
                    .ThenInclude(shipmentLeg => shipmentLeg.Load)
                    .Include(status => status.ShipmentLeg.PickUpStop)
                    .Include(status => status.ShipmentLeg.DropOffStop)
                    .Where(status => status.Id == recordId)
                    .OrderByDescending(status => status.UpdatedAt)
                    .Take(1)
                    .ToList();

                var shipmentLegStatus = tmsStatuses.FirstOrDefault();

                // Build payload.
                FujiFilmXml xmlBuilder = new(shipmentLegStatus);
                var xDoc = xmlBuilder.Build();

                if (shipmentLegStatus == null) return xDoc;

                Log.Debug(
                    $"#{shipmentLegStatus.Id} - {shipmentLegStatus.ShipmentLeg?.ShipperReference} - {shipmentLegStatus.ShipmentLeg?.Load?.LoadGroup} - {shipmentLegStatus.ShipmentLeg?.PickUpStop?.LocationCity} - {shipmentLegStatus.ShipmentLeg?.DropOffStop?.LocationCity}");

                return xDoc;
            }
            catch (Exception e)
            {
                Log.Error(e, "Error retrieving change status record entry.");
                throw;
            }
        }

        /// <summary>
        ///     Any finalizing tasks to be done with current XML payload before
        ///     moving on to the next. (i.e. emails, ftp upload, etc.)
        /// </summary>
        /// <param name="payload"></param>
        protected override bool HandlePayload(XDocument payload)
        {
            try
            {
                // Send payload to EDIConverter to return EDI converted text.
                var ediPayload = EdiServiceConnector.ConvertXmlToEdi(payload, Configuration.XmlToEdiServiceAddress, Configuration.X12Version, Configuration.X12Document);

                SftpManager sftp = new(
                    Configuration.Host,
                    Convert.ToInt32(Configuration.Port),
                    Configuration.Username,
                    Configuration.Password);

                sftp.Upload(ediPayload, Configuration.FtpDirectory, SftpManager.GetTimeStampedFileName(Configuration.Filename));

                return true;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error uploading to Ftp. Check Configuration data to ensure required inputs are filled in.\n" +
                             $"Service Address: {Configuration.XmlToEdiServiceAddress}\n" +
                             $"X12Version: {Configuration.X12Version} and X12Document {Configuration.X12Document}");
                throw;
            }
        }
    }
}