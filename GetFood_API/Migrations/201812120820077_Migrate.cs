namespace GetFood_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Foods", new[] { "RestaurantId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            AddColumn("dbo.Orders", "RestaurantId", c => c.Int());
            AlterColumn("dbo.Foods", "PrepTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Foods", "RestaurantId", c => c.Int());
            AlterColumn("dbo.Orders", "DriverId", c => c.Int());
            AlterColumn("dbo.Orders", "PickupTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "DeliveryTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Foods", "RestaurantId");
            CreateIndex("dbo.Orders", "DriverId");
            CreateIndex("dbo.Orders", "RestaurantId");
            AddForeignKey("dbo.Orders", "RestaurantId", "dbo.Restaurants", "RestaurantId");
            AddForeignKey("dbo.Foods", "RestaurantId", "dbo.Restaurants", "RestaurantId");
            AddForeignKey("dbo.Orders", "DriverId", "dbo.Drivers", "DriverId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Foods", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Orders", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Orders", new[] { "RestaurantId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Foods", new[] { "RestaurantId" });
            AlterColumn("dbo.Orders", "DeliveryTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Orders", "PickupTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Orders", "DriverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Foods", "RestaurantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Foods", "PrepTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Orders", "RestaurantId");
            CreateIndex("dbo.Orders", "DriverId");
            CreateIndex("dbo.Foods", "RestaurantId");
            AddForeignKey("dbo.Orders", "DriverId", "dbo.Drivers", "DriverId", cascadeDelete: true);
            AddForeignKey("dbo.Foods", "RestaurantId", "dbo.Restaurants", "RestaurantId", cascadeDelete: true);
        }
    }
}
