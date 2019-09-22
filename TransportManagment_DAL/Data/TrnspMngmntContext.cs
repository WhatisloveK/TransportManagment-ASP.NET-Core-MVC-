using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagment_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TransportManagment_DAL.Data
{
    public class TrnspMngmntContext : IdentityDbContext<Company>
    {
        public TrnspMngmntContext(DbContextOptions<TrnspMngmntContext> options) : base(options)
        {
        }

        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<TruckType> TruckTypes { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Company> Companies { get; set; }
        //public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TruckType>().ToTable("TruckType");
            modelBuilder.Entity<Cargo>().ToTable("Cargo");
            modelBuilder.Entity<Transport>().ToTable("Transport");
            //modelBuilder.Entity<Company>().ToTable("Company");

            //modelBuilder.Entity<Student>().ToTable("Student");
        }
        //public DbSet<Student> Students { get; set; }
        //public DbSet<TransportManagment.Models.Transport> Transport { get; set; }
    }
}
