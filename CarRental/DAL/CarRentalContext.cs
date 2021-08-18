using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CarRental.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarRental.DAL
{
    public class CarRentalContext : IdentityDbContext<ApplicationUser>
    {
        public CarRentalContext() : base("CarRentalContext") { }

        static CarRentalContext()
        {
            //Database.SetInitializer<CarRentalContext>(new CarRentaInitializer());
        }

        public static CarRentalContext Create()
        {
            return new CarRentalContext();
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}