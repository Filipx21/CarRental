namespace CarRental.Migrations
{
    using CarRental.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<CarRental.DAL.CarRentalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CarRental.DAL.CarRentalContext";
        }

        protected override void Seed(CarRental.DAL.CarRentalContext context)
        {
            CarRentaInitializer.SeedCarRental(context);
            CarRentaInitializer.SeedUsers(context);
        }
    }
}
