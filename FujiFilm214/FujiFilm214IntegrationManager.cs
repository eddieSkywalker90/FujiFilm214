using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FujiFilm214.ChemStarDb.Data;
using JankyIntegrationManager;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FujiFilm214
{
    public class FujiFilm214IntegrationManager : DeltaCheckIntegrationManager<XDocument>
    {
        public FujiFilm214IntegrationManager() : base(Configuration.Root)
        {
        }

        /// <summary>
        ///     Call associated SQL View to return list of potentially new/updated records.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns>A List of record id's.</returns>
        protected override List<string> IdentifyPotentiallyChangedRecordIds(DateTime startTime)
        {
            Console.WriteLine("IdentifyPotentiallyChangedRecordIds() - Returning list of record entries..\n");

            // Access ChemStar DB to pull data
            using ChemStarDbContext dbContext = new();
            var changedStatuses = dbContext.VwTmsShipmentLegStatusesV1s.Take(3).ToList();

            // Grab each entries ID as the RecordId.
            List<string> changedStatusIds = new();
            foreach (var status in changedStatuses) changedStatusIds.Add(status.Id);
            Console.WriteLine($"Updated Statuses Count: {changedStatusIds.Count}");
            Console.WriteLine($"Updated Statuses: {string.Join(',', changedStatusIds)}");

            return changedStatusIds;
        }

        /// <summary>
        ///     Using the collection of returned record id's generated from IdentifyPotentiallyChangedRecordIds(),
        ///     query the SQLDW database to create a payload from joining matching recordID's in associated tables.
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        protected override XDocument? GetRecordPayload(string statusId)
        {
            Log.Information("---------------------------------------");
            Log.Information($"GetRecordPayload - {statusId}");

            //Query the relevant data for the statusId
            using ChemStarDbContext dbContext = new();
            var tmsStatuses = dbContext.VwTmsShipmentLegStatusesV1s
                .Include(status => status.ShipmentLeg)
                .ThenInclude(shipmentLeg => shipmentLeg.Load)
                .Include(status => status.ShipmentLeg.PickUpStop)
                .Include(status => status.ShipmentLeg.DropOffStop)
                .Where(status => status.Id == statusId)
                .OrderByDescending(status => status.UpdatedAt)
                .Take(1)
                .ToList();
            var tmsStatus = tmsStatuses.FirstOrDefault();

            if (tmsStatus != null)
            {
                //Build out payload with the new data
                var xDocument = tmsStatus.BuildFujiFilm214Xml();
                Log.Information(
                    $"{tmsStatus.Id} - {tmsStatus.ShipmentLeg?.ShipperReference} - {tmsStatus.ShipmentLeg?.Load?.LoadGroup} - {tmsStatus.ShipmentLeg?.PickUpStop?.LocationCity} - {tmsStatus.ShipmentLeg?.DropOffStop?.LocationCity}");
                Log.Information(
                    $"\n{xDocument}");
                return xDocument;
            }

            return null;
        }
    }
}