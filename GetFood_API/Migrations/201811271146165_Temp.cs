namespace GetFood_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Temp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DeliveryTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "PickupTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PickupTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Orders", "DeliveryTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
