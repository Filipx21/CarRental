namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Car", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Car", "LastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Car", "LastUpdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Car", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
