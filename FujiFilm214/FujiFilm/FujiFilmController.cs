using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FujiFilm214.ChemStarDb.Data;
using JankyIntegrationManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FujiFilm214.FujiFilm
{
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
        protected override List<string> IdentifyPotentiallyChangedRecordIds(DateTime startTime)
        {
            // Access ChemStar DB to pull data
            using ChemStarDbContext dbContext = new();
            var changedStatuses = dbContext.VwTmsShipmentLegStatusesV1s.Take(3).ToList();

            // Grab each entries ID as the RecordId.
            List<string> newOrUpdatedRecordsIds = new();
            foreach (var status in changedStatuses)
            {
                newOrUpdatedRecordsIds.Add(status.Id);

                // Dev-only environment.
                if (Configuration.Environment.Equals("Development"))
                    Log.Debug($"New/Updated Status RecordID: {status.Id}");
            }

            Log.Debug(
                $"IdentifyPotentiallyChangedRecordIds() - Returning list of {changedStatuses.Count} record entries..");

            return newOrUpdatedRecordsIds;
        }

        /// <summary>
        ///     Using the collection of returned record id's generated from IdentifyPotentiallyChangedRecordIds(),
        ///     query the SQLDW database to create a payload from joining matching recordID's in associated tables.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        protected override XDocument GetRecordPayload(string recordId)
        {
            Log.Debug($"GetRecordPayload() - Building XML for record #: {recordId}..");

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
            FujiFilmXml xmlBuilder = new(shipmentLegStatus);
            var xDoc = xmlBuilder.Build();

            if (shipmentLegStatus != null)
            {
                Log.Information(
                    $"{shipmentLegStatus.Id} - {shipmentLegStatus.ShipmentLeg?.ShipperReference} - {shipmentLegStatus.ShipmentLeg?.Load?.LoadGroup} - {shipmentLegStatus.ShipmentLeg?.PickUpStop?.LocationCity} - {shipmentLegStatus.ShipmentLeg?.DropOffStop?.LocationCity}");
                Log.Information(
                    $"\n{xDoc}");
            }
            // Log.Debug(shipmentLegStatus.ShipmentLeg.PickUpStop.Id);

            return xDoc;
        }

        /// <summary>
        ///     Any finalizing tasks to be done with current XML payload before
        ///     moving on to the next. (i.e. emails, ftp upload, etc.)
        /// </summary>
        /// <param name="payload"></param>
        protected override void HandlePayload(XDocument payload)
        {
            SftpManager sftp = new(
                Configuration.Host,
                Convert.ToInt32(Configuration.Port), 
                Configuration.Username, 
                Configuration.Password,
                Configuration.Filename,
                Configuration.FtpDirectory,
                Configuration.AlternateFtpDirectory);

            sftp.Upload(payload);
        }
    }
}