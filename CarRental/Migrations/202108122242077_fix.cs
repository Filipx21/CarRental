namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car", "CostPerDay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Car", "KilometerCost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Car", "KilometerCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Car", "CostPerDay");
        }
    }
}
