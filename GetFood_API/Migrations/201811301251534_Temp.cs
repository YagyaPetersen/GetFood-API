namespace GetFood_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Temp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.DriverId);
            
            CreateTable(
                "dbo.FoodOrders",
                c => new
                    {
                        FoodOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodOrderId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        FoodName = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrepTime = c.Time(nullable: false, precision: 7),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        DriverAcceptance = c.Boolean(nullable: false),
                        RestaurantAcceptance = c.Boolean(nullable: false),
                        PickupTime = c.Time(nullable: false, precision: 7),
                        OrderStatus = c.String(nullable: false),
                        DeliveryFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverallFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeliveryTime = c.Time(nullable: false, precision: 7),
                        CustomerAddress = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FoodOrders", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.Foods", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Foods", new[] { "RestaurantId" });
            DropIndex("dbo.FoodOrders", new[] { "FoodId" });
            DropIndex("dbo.FoodOrders", new[] { "OrderId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Foods");
            DropTable("dbo.FoodOrders");
            DropTable("dbo.Drivers");
            DropTable("dbo.Customers");
        }
    }
}
