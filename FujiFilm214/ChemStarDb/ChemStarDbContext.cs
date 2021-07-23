using System;
using FujiFilm214.ChemStarDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#nullable disable

namespace FujiFilm214.ChemStarDb.Data
{
    public class ChemStarDbContext : DbContext
    {
        public ChemStarDbContext()
        {
        }

        public ChemStarDbContext(DbContextOptions<ChemStarDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VwTmsShipmentLegStatusesV1> VwTmsShipmentLegStatusesV1s { get; set; }
        public virtual DbSet<VwTmsShipmentLegsV1> VwTmsShipmentLegsV1s { get; set; }

        public static readonly LoggerFactory _myLoggerFactory =
            new();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(_myLoggerFactory);
                optionsBuilder.UseSqlServer(Configuration.ChemStarDbConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            //Set model views and keys
            modelBuilder.Entity<VwTmsShipmentLegStatusesV1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToView("vw_TmsShipmentLegStatuses_V1");
            });
            modelBuilder.Entity<VwTmsShipmentLegsV1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToView("vw_TmsShipmentLegs_V1");
            });
            modelBuilder.Entity<VwTmsLoadsV1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToView("vw_TmsLoads_V1");
            });
            modelBuilder.Entity<VwTmsLoadStopsV1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToView("vw_TmsLoadStops_V1");
            });

            //Set model relationships
            modelBuilder.Entity<VwTmsShipmentLegStatusesV1>()
                .HasOne(legstatus => legstatus.ShipmentLeg)
                .WithMany(leg => leg.ShipmentLegStatuses)
                .HasForeignKey(status => status.ShipmentLegId);

            modelBuilder.Entity<VwTmsShipmentLegsV1>()
                .HasOne(leg => leg.Load)
                .WithMany(load => load.ShipmentLegs)
                .HasForeignKey(leg => leg.LoadId);

            modelBuilder.Entity<VwTmsShipmentLegsV1>()
                .HasOne(leg => leg.PickUpStop)
                .WithMany(stop => stop.PickUpShipmentLegs)
                .HasForeignKey(leg => leg.PickUpStopId);
            modelBuilder.Entity<VwTmsShipmentLegsV1>()
                .HasOne(leg => leg.DropOffStop)
                .WithMany(stop => stop.DropOffShipmentLegs)
                .HasForeignKey(leg => leg.DropOffStopId);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
