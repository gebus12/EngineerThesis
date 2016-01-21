using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.DAL
{
    public class TransportPublicContext : DbContext
    {
        public TransportPublicContext()
            : base("TransportPublicContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>().HasMany(b => b.Drivers).WithMany(d => d.Buses).Map(m =>
            {
                m.MapLeftKey("BusID");
                m.MapRightKey("DriverID");
                m.ToTable("BusDriver");
            });
            modelBuilder.Entity<Bus>().HasMany(b => b.Lines).WithMany(l => l.Buses).Map(m =>
           {
               m.MapLeftKey("BusID");
               m.MapRightKey("LineID");
               m.ToTable("BusLine");
           });
            //modelBuilder.Entity<Driver>().HasMany(b => b.Lines).WithMany(d => d.Drivers).Map(m =>
            //{
            //    m.MapLeftKey("DriverID");
            //    m.MapRightKey("LineID");
            //    m.ToTable("DriverLine");
            //});
        }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }

        public virtual DbSet<History> Histories { get; set; }

        public DbSet<MsgDriver> MsgDrivers { get; set; }

        public DbSet<MsgPassenger> MsgPassengers { get; set; }


    }
}