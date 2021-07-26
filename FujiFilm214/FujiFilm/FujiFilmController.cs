using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FujiFilm214.ChemStarDb.Data;
using JankyIntegrationManager;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FujiFilm214.FujiFilm
{
    public class FujiFilmController : DeltaCheckIntegrationManager<MemoryStream>
    {
        /// <summary>
        ///     Call associated SQL View to return list of potentially new/updated records.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns>A List of record id's.</returns>
        protected override List<string> IdentifyPotentiallyChangedRecordIds(DateTime startTime)
        {
            // Access ChemStar DB to pull data
            using ChemStarDbContext dbContext = new();
            var wmsTmsOrders = dbContext.VwTmsShipmentLegStatusesV1s.Take(3).ToList();

            // Grab each entries ID as the RecordId.
            List<string> newOrUpdatedRecords = new();
            foreach (var recordEntry in wmsTmsOrders)
            {
                newOrUpdatedRecords.Add(recordEntry.Id);

                // Dev-only environment.
                if (Configuration.Environment.Equals("Development")) Log.Debug($"New/Updated RecordIDs: {recordEntry.Id}");
            }
            Log.Debug($"IdentifyPotentiallyChangedRecordIds() - Returning list of { wmsTmsOrders.Count} record entries..");

            return newOrUpdatedRecords;
        }

        /// <summary>
        ///     Using the collection of returned record id's generated from IdentifyPotentiallyChangedRecordIds(),
        ///     query the SQLDW database to create a payload from joining matching recordID's in associated tables.
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="writer"></param>
        /// <returns></returns>
        protected override MemoryStream GetRecordPayload(string recordId)
        {
            Log.Debug($"GetRecordPayload() - Building XML for record #: {recordId}..");
            MemoryStream xmlDataStream = new();
            FujiFilmXml xml = new();
            xml.Build(xmlDataStream, recordId);
            Log.Debug(Encoding.UTF8.GetString(xmlDataStream.ToArray()) + "\n");

            #region Old Soon To Be Deleted..

            // var shipmentLegStatuses = dbContext.VwTmsShipmentLegStatusesV1s.Select(order => order.Id.Equals("617383252"));
            // var shipmentLegs = dbContext.VwTmsShipmentLegsV1s.Select(order => order.Id.Equals("279165848"));
            // var pickUpStops = dbContext.VwTmsLoadStopsV1s.Select(order => order.Id.Equals("274489973"));
            // var dropOffStops = dbContext.VwTmsLoadStopsV1s.Select(order => order.Id.Equals("274489974"));
            // var loadIds = dbContext.VwTmsLoadsV1s.Select(order => order.Id.Equals("125146648"));


            // var legList = dbContext.VwTmsLoadsV1s
            //     .Take(10)
            //     .Include(load => load.ShipmentLegs).ToList();
            //
            // foreach (var leg in legList)
            // {
            //     Console.WriteLine("LegID: " + leg.Id);
            // }


            // ID: 617383252
            // var legStatus = dbContext.VwTmsLoadsV1s
            //     .Where(load => load.Id.Equals(617383252))
            //     .Where()


            // // Returns list containing all items matching recordId. Typically 1 response, but could be multiple.
            // var orderLines = dbContext.VwWmsTmsOrderLines.Where(order => order.Id.ToString().Equals(recordId)).ToList();
            //
            // if (orderLines.Count == 1)
            // {
            //     // Combine tables and build into payload.
            //     payload.Id = orderLines.First().Id;
            //     payload.Sku = orderLines.First().Sku;
            //     payload.EstimatedShipDate = orderLines.First().ModStamp;
            //
            //     return payload;
            // }
            //
            // // Grab most recent entry [by date] as the payload.
            // var firstPass = true;
            // var mostRecentDate = DateTime.UnixEpoch;
            // foreach (var order in orderLines)
            //     if (firstPass)
            //     {
            //         mostRecentDate = order.CreateStamp;
            //         firstPass = false;
            //     }
            //     else
            //     {
            //         if (order.CreateStamp > mostRecentDate) mostRecentDate = order.CreateStamp;
            //     }
            //
            // var latestOrderLine = orderLines.Find(order => order.ModStamp.Equals(mostRecentDate));
            // payload.Id = latestOrderLine.Id;
            // payload.Sku = latestOrderLine.Sku;
            // payload.EstimatedShipDate = latestOrderLine.ModStamp;

            #endregion

            return xmlDataStream;
        }

        public FujiFilmController(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
        }
    }
}