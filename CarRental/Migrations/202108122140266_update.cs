namespace CarRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Street", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_Telefon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Telefon", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_Phone");
            DropColumn("dbo.AspNetUsers", "UserData_Street");
        }
    }
}
