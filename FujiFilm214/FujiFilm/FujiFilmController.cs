using System;
using System.Collections.Generic;
using System.Linq;
using FujiFilm214.ChemStarDb.Models;
using FujiFilm214.FujiFilm.Models;
using JankyIntegrationManager;
using Microsoft.EntityFrameworkCore;

namespace FujiFilm214.FujiFilm
{
    public class FujiFilmController : DeltaCheckIntegrationManager<Payload>
    {
        /// <summary>
        ///     Call associated SQL View to return list of potentially updated payload records.
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        protected override List<string> IdentifyPotentiallyChangedRecordIds(DateTime startTime)
        {
            // Access ChemStar DB to pull data
            using ChemStarDbContext dbContext = new();
            var wmsTmsOrders = dbContext.VwTmsShipmentLegStatusesV1s.Take(10).ToList();

            List<string> newOrUpdatedRecords = new();
            foreach (var recordEntry in wmsTmsOrders)
            {
                newOrUpdatedRecords.Add(recordEntry.Id);
            }

            Console.WriteLine("WmsTmsOrders Count: " + wmsTmsOrders.Count);
            Console.WriteLine("IdentifyPotentiallyChangedRecordIds() - Returning list of record entries..");

            return newOrUpdatedRecords;
        }

        /// <summary>
        ///     Using the collection of returned recordId's generated from IdentifyPotentiallyChangedRecordIds(),
        ///     query the SQLDW database to create a payload from joining matching recordID's in ShipmentStatus
        ///     + ShipmentStatusLines tables. //TODO table names and quantity subject to change. Update as needed.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        protected override Payload GetRecordPayload(string recordId)
        {
            Payload payload = new();

            using ChemStarDbContext dbContext = new();

            var shipmentLegStatuses = dbContext.VwTmsShipmentLegStatusesV1s.Select(order => order.Id.Equals("617383252"));
            var shipmentLegs = dbContext.VwTmsShipmentLegsV1s.Select(order => order.Id.Equals("279165848"));
            var pickUpStops = dbContext.VwTmsLoadStopsV1s.Select(order => order.Id.Equals("274489973"));
            var dropOffStops = dbContext.VwTmsLoadStopsV1s.Select(order => order.Id.Equals("274489974"));
            var loadIds = dbContext.VwTmsLoadsV1s.Select(order => order.Id.Equals("125146648"));

            var something = dbContext.VwTmsLoadsV1s
                .Include(load => load);


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

            return payload;

            // // Join both lists into one, tracking property X.
            // var wmsTmsOrders = dbContext.VwWmsTmsOrders.Take(10).ToList();
            // var wmsTmsOrderLines = dbContext.VwWmsTmsOrderLines.Take(10).ToList();
            // List<string> returnedOrdersRecords = new();
            // foreach (var order in wmsTmsOrders) returnedOrdersRecords.Add(order.Id);
            // foreach (var order in wmsTmsOrderLines) returnedOrdersRecords.Add(order.Id.ToString());
            // Console.WriteLine("Total WMSORders: " + wmsTmsOrders.Count + " " + wmsTmsOrders.First().Status + " " + wmsTmsOrders.First().Id);
            // Console.WriteLine("Total WMSOrderLines: " + wmsTmsOrderLines.Count + " " + wmsTmsOrderLines.First().Status + " " + wmsTmsOrderLines.First().Id);
            //
            // return payload;
        }
    }
}