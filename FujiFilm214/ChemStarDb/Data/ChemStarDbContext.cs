using System;
using FujiFilm214.ChemStarDb.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FujiFilm214.ChemStarDb.Data
{
    public sealed class ChemStarDbContext : DbContext
    {
        public ChemStarDbContext()
        {
            Database.SetCommandTimeout(180);
        }

        public ChemStarDbContext(DbContextOptions<ChemStarDbContext> options)
            : base(options)
        {
        }

        public DbSet<VwTmsLoadStopsV1> VwTmsLoadStopsV1s { get; set; }
        public DbSet<VwTmsLoadsV1> VwTmsLoadsV1s { get; set; }
        public DbSet<VwTmsShipmentLegStatusesV1> VwTmsShipmentLegStatusesV1s { get; set; }
        public DbSet<VwTmsShipmentLegsV1> VwTmsShipmentLegsV1s { get; set; }
        public DbSet<VwWmsTmsOrder> VwWmsTmsOrders { get; set; }
        public DbSet<VwWmsTmsOrderLine> VwWmsTmsOrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(Configuration.ChemStarDbConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            // modelBuilder.Entity<VwTmsLoadStopsV1>()
            //     .HasOne(loadstop => loadstop.Load)
            //     .WithMany(load => load.ShipmentLoadStops)
            //     .HasForeignKey(loadstop => loadstop.Id);

            modelBuilder.Entity<VwTmsLoadStopsV1>(entity =>
            {
                // entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToView("vw_TmsLoadStops_V1");

                entity.Property(e => e.Address1).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(255);

                entity.Property(e => e.Address3).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(55);

                entity.Property(e => e.FileDate).HasColumnType("datetime");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IsPoolPoint).HasMaxLength(5);

                entity.Property(e => e.LoadId).HasMaxLength(50);

                entity.Property(e => e.LocationAddress1).HasMaxLength(255);

                entity.Property(e => e.LocationAddress2).HasMaxLength(255);

                entity.Property(e => e.LocationAddress3).HasMaxLength(255);

                entity.Property(e => e.LocationCity).HasMaxLength(255);

                entity.Property(e => e.LocationCountry).HasMaxLength(55);

                entity.Property(e => e.LocationId).HasMaxLength(55);

                entity.Property(e => e.LocationName).HasMaxLength(255);

                entity.Property(e => e.LocationRef).HasMaxLength(55);

                entity.Property(e => e.LocationState).HasMaxLength(55);

                entity.Property(e => e.MergeDate).HasMaxLength(55);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.State).HasMaxLength(55);

                entity.Property(e => e.StopType).HasMaxLength(25);
            });

            modelBuilder.Entity<VwTmsShipmentLegStatusesV1>()
                .HasOne(legstatus => legstatus.ShipmentLeg)
                .WithMany(leg => leg.ShipmentLegStatuses)
                .HasForeignKey(status => status.Id);

            modelBuilder.Entity<VwTmsShipmentLegStatusesV1>(entity =>
            {
                // entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToView("vw_TmsShipmentLegStatuses_V1");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DataSource).HasMaxLength(50);

                entity.Property(e => e.DataSourceType).HasMaxLength(40);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ReasonCode).HasMaxLength(100);

                entity.Property(e => e.ReasonCodeId).HasMaxLength(5);

                entity.Property(e => e.ShipmentId)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.StatusAction).HasMaxLength(50);

                entity.Property(e => e.StatusCode).HasMaxLength(100);

                entity.Property(e => e.StatusCodeId).HasMaxLength(5);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwTmsShipmentLegsV1>(entity =>
            {
                // entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToView("vw_TmsShipmentLegs_V1");

                entity.Property(e => e.BillableAllocation).HasColumnType("decimal(16, 4)");

                entity.Property(e => e.BillableAllocationCurrencyCode).HasMaxLength(5);

                entity.Property(e => e.DistanceUnit).HasMaxLength(5);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PayableAllocation).HasColumnType("decimal(16, 4)");

                entity.Property(e => e.PayableAllocationCurrencyCode).HasMaxLength(5);

                entity.Property(e => e.PickUpStopId).HasMaxLength(50);

                entity.Property(e => e.ScheduleIntegrationKey).HasMaxLength(50);

                entity.Property(e => e.ShipmentId)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.ShipperReference).HasMaxLength(50);

                entity.Property(e => e.StatusDescription).HasMaxLength(50);

                entity.Property(e => e.TmsPlanningAbility).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            // modelBuilder.Entity<VwTmsShipmentLegsV1>()
            //     .HasOne(leg => leg.Load)
            //     .WithMany(load => load.ShipmentLegs)
            //     .HasForeignKey(leg => leg.Id);

            modelBuilder.Entity<VwTmsLoadsV1>(entity =>
            {
                // entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToView("vw_TmsLoads_V1");

                entity.Property(e => e.CarrierName).HasMaxLength(255);

                entity.Property(e => e.CarrierScac).HasMaxLength(15);

                entity.Property(e => e.ContainerNumber).HasMaxLength(55);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Density).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DensityUom).HasMaxLength(5);

                entity.Property(e => e.DirectionCategory).HasMaxLength(255);

                entity.Property(e => e.Distance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DistanceUom).HasMaxLength(5);

                entity.Property(e => e.Division).HasMaxLength(55);

                entity.Property(e => e.DriverName).HasMaxLength(55);

                entity.Property(e => e.Equipment).HasMaxLength(55);

                entity.Property(e => e.EquipmentCode).HasMaxLength(10);

                entity.Property(e => e.EquipmentType).HasMaxLength(25);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsCanceled).HasMaxLength(55);

                entity.Property(e => e.LastExecutedEvent).HasMaxLength(55);

                entity.Property(e => e.LastExecutedEventId).HasMaxLength(5);

                entity.Property(e => e.LinearSpace).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LinearSpaceUom).HasMaxLength(5);

                entity.Property(e => e.LoadGroup).HasMaxLength(55);

                entity.Property(e => e.Mode).HasMaxLength(50);

                entity.Property(e => e.PalletQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PieceQuantity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ServiceLevel).HasMaxLength(255);

                entity.Property(e => e.TourId)
                    .HasMaxLength(55)
                    .HasColumnName("TourID");

                entity.Property(e => e.TrailerNumber).HasMaxLength(55);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.VehicleNumber).HasMaxLength(55);

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VolumeUom).HasMaxLength(5);

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WeightUom).HasMaxLength(5);
            });

            modelBuilder.Entity<VwWmsTmsOrder>(entity =>
            {
                // entity.HasNoKey();

                entity.HasKey(e => e.Id);

                entity.ToView("vw_WmsTmsOrders");

                entity.Property(e => e.CarrierService)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("carrier_service");

                entity.Property(e => e.CloseStamp).HasColumnName("close_stamp");

                entity.Property(e => e.Comments)
                    .HasMaxLength(255)
                    .HasColumnName("comments");

                entity.Property(e => e.CreateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("create_stamp");

                entity.Property(e => e.FreightPaymentTerms)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("freight_payment_terms");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("id");

                entity.Property(e => e.ModStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("mod_stamp");

                entity.Property(e => e.OwnerCode)
                    .HasMaxLength(255)
                    .HasColumnName("owner_code");

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(255)
                    .HasColumnName("po_number");

                entity.Property(e => e.RequestedShipDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requested_ship_date");

                entity.Property(e => e.ScheduledShipDate)
                    .HasColumnType("datetime")
                    .HasColumnName("scheduled_ship_date");

                entity.Property(e => e.ShipFromCity)
                    .HasMaxLength(20)
                    .HasColumnName("ship_from_city");

                entity.Property(e => e.ShipFromCountry)
                    .HasMaxLength(20)
                    .HasColumnName("ship_from_country");

                entity.Property(e => e.ShipFromName)
                    .HasMaxLength(40)
                    .HasColumnName("ship_from_name");

                entity.Property(e => e.ShipFromPostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("ship_from_postal_code");

                entity.Property(e => e.ShipFromState)
                    .HasMaxLength(20)
                    .HasColumnName("ship_from_state");

                entity.Property(e => e.ShipFromStreet1)
                    .HasMaxLength(40)
                    .HasColumnName("ship_from_street1");

                entity.Property(e => e.ShipFromStreet2)
                    .HasMaxLength(40)
                    .HasColumnName("ship_from_street2");

                entity.Property(e => e.ShipFromStreet3).HasColumnName("ship_from_street3");

                entity.Property(e => e.ShipFromWarehouseCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("ship_from_warehouse_code");

                entity.Property(e => e.ShipToCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ship_to_city");

                entity.Property(e => e.ShipToCountry)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("ship_to_country");

                entity.Property(e => e.ShipToName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ship_to_name");

                entity.Property(e => e.ShipToPostalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("ship_to_postal_code");

                entity.Property(e => e.ShipToState)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("ship_to_state");

                entity.Property(e => e.ShipToStreet1)
                    .HasMaxLength(50)
                    .HasColumnName("ship_to_street1");

                entity.Property(e => e.ShipToStreet2)
                    .HasMaxLength(50)
                    .HasColumnName("ship_to_street2");

                entity.Property(e => e.ShipToStreet3)
                    .HasMaxLength(50)
                    .HasColumnName("ship_to_street3");

                entity.Property(e => e.ShipToWarehouseCode).HasColumnName("ship_to_warehouseCode");

                entity.Property(e => e.Shipment)
                    .HasMaxLength(10)
                    .HasColumnName("shipment");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.Stage)
                    .HasMaxLength(255)
                    .HasColumnName("stage");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.SupplierCode)
                    .HasMaxLength(255)
                    .HasColumnName("supplier_code");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("type");

                entity.Property(e => e.Wave)
                    .HasMaxLength(10)
                    .HasColumnName("wave");
            });

            modelBuilder.Entity<VwWmsTmsOrderLine>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_WmsTmsOrderLines");

                entity.Property(e => e.CloseStamp).HasColumnName("close_stamp");

                entity.Property(e => e.Comments)
                    .HasMaxLength(255)
                    .HasColumnName("comments");

                entity.Property(e => e.CreateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("create_stamp");

                entity.Property(e => e.EndUserPartNumber)
                    .HasMaxLength(30)
                    .HasColumnName("end_user_part_number");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lot)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lot");

                entity.Property(e => e.ManufacturerPartNumber)
                    .HasMaxLength(30)
                    .HasColumnName("manufacturer_part_number");

                entity.Property(e => e.ModStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("mod_stamp");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("order_id");

                entity.Property(e => e.PmDescription)
                    .HasMaxLength(40)
                    .HasColumnName("pm_description");

                entity.Property(e => e.PmEmptyQtyUom1InUom2)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_empty_qty_uom1_in_uom2");

                entity.Property(e => e.PmEmptyQtyUom2InUom3)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_empty_qty_uom2_in_uom3");

                entity.Property(e => e.PmEmptyUom1)
                    .HasMaxLength(10)
                    .HasColumnName("pm_empty_uom1");

                entity.Property(e => e.PmEmptyUom2)
                    .HasMaxLength(10)
                    .HasColumnName("pm_empty_uom2");

                entity.Property(e => e.PmEmptyUom3)
                    .HasMaxLength(10)
                    .HasColumnName("pm_empty_uom3");

                entity.Property(e => e.PmEmptyWeight)
                    .HasColumnType("decimal(17, 5)")
                    .HasColumnName("pm_empty_weight");

                entity.Property(e => e.PmHazardClass)
                    .HasMaxLength(5)
                    .HasColumnName("pm_hazard_class");

                entity.Property(e => e.PmIsHazmat).HasColumnName("pm_is_hazmat");

                entity.Property(e => e.PmIsTempControlled).HasColumnName("pm_is_temp_controlled");

                entity.Property(e => e.PmQtyUom1InUom2)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_qty_uom1_in_uom2");

                entity.Property(e => e.PmQtyUom2InUom3)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_qty_uom2_in_uom3");

                entity.Property(e => e.PmQtyUom3InUom4)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_qty_uom3_in_uom4");

                entity.Property(e => e.PmQtyUom4InUom5)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_qty_uom4_in_uom5");

                entity.Property(e => e.PmQtyUom5InUom6)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("pm_qty_uom5_in_uom6");

                entity.Property(e => e.PmUom1)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom1");

                entity.Property(e => e.PmUom2)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom2");

                entity.Property(e => e.PmUom3)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom3");

                entity.Property(e => e.PmUom4)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom4");

                entity.Property(e => e.PmUom5)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom5");

                entity.Property(e => e.PmUom6)
                    .HasMaxLength(10)
                    .HasColumnName("pm_uom6");

                entity.Property(e => e.PmWeight)
                    .HasColumnType("decimal(17, 5)")
                    .HasColumnName("pm_weight");

                entity.Property(e => e.PoNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("po_number");

                entity.Property(e => e.Quantity)
                    .HasColumnType("decimal(12, 3)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("sku");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("status");

                entity.Property(e => e.TmsLoadIds).HasColumnName("tms_load_ids");

                entity.Property(e => e.Uom)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("uom");

                entity.Property(e => e.WarehouseCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("warehouse_code");
            });

            // OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
