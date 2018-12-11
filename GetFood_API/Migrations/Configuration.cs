using System.Data.Entity.Migrations.Infrastructure;
using GetFood_API.Classes;

namespace GetFood_API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GetFood_API.GetFoodContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GetFood_API.GetFoodContext context)
        {
            context.Restaurants.AddOrUpdate(
                a => a.RestaurantId,
                new Restaurant() { RestaurantId = 1, RestaurantName = "McDonald's", Address = "Cnr Kloof & Orange Streets, Gardens, Cape Town" },
                new Restaurant() { RestaurantId = 2, RestaurantName = "KFC", Address = "Mill Street, Corner Buitekant Street, Gardens, Cape Town" },
                new Restaurant() { RestaurantId = 3, RestaurantName = "Nandos", Address = "Adderley St, Shop 6V, Grand Parade Centre, Cape Town City Centre, Cape Town" },
                new Restaurant() { RestaurantId = 4, RestaurantName = "Steers", Address = "Shop 90 Concourse Level, Adderley Street, Golden Acre, Cape Town" },
                new Restaurant() { RestaurantId = 4, RestaurantName = "Debonairs", Address = "Shop 2 London House, 128 Main Road, Sea Point, Cape Town" }
                );

            context.Foods.AddOrUpdate(
                a => a.FoodId,
                new Food() { FoodId = 1, FoodName = "BigMac", Description = "The one, the only! Enjoy a succulent 100% beef patty, cheese, onion, and crisp lettuce in a sesame seed bun.", RestaurantId = 1, Price = 40, PrepTime = DateTime.Parse("00:25:00") },
                new Food() { FoodId = 2, FoodName = "McChicken", Description = "A tender chicken breast coated in a delicious, seasoned batter, and perfectly cooked for your satisfaction", RestaurantId = 1, Price = 35, PrepTime = DateTime.Parse("00:30:00") },
                new Food() { FoodId = 3, FoodName = "4pc McNuggets", Description = "4 piece melt-in-your-mouth tender chicken breast nuggets coated and cooked in a delicious, seasoned batter.", RestaurantId = 1, Price = 30, PrepTime = DateTime.Parse("00:20:00") },

                new Food() { FoodId = 4, FoodName = "StreetWise 2", Description = "2 Pieces of chicken cooked to golden perfection and a small portion of chips.", RestaurantId = 2, Price = 30, PrepTime = DateTime.Parse("00:25:00") },
                new Food() { FoodId = 5, FoodName = "Zinger Burger", Description = "A spicy Zinger fillet topped with fresh lettuce a slice of tomato and Colonel dressing served on a fresh bun.", RestaurantId = 2, Price = 40, PrepTime = DateTime.Parse("00:30:00") },
                new Food() { FoodId = 6, FoodName = "Dunked Wings", Description = "4 KFC famous Zinger Wings dunked in a delicious honey ginger and soy sauce.", RestaurantId = 2, Price = 30, PrepTime = DateTime.Parse("00:15:00") },

                new Food() { FoodId = 7, FoodName = "1/4 Chicken Meal", Description = "1/4 Chicken, single side and soft drink can", RestaurantId = 3, Price = 70, PrepTime = DateTime.Parse("00:35:00") },
                new Food() { FoodId = 8, FoodName = "HotPot", Description = "Pulled Chicken, Nando's spicy rice, veggies and tomato relish", RestaurantId = 3, Price = 37, PrepTime = DateTime.Parse("00:20:00") },
                new Food() { FoodId = 9, FoodName = "Chicken Wrap", Description = "Chicken Wrap with a single portion side of your choice", RestaurantId = 3, Price = 45, PrepTime = DateTime.Parse("00:25:00") },

                new Food() { FoodId = 10, FoodName = "King Steer Burger", Description = "3 beef patties & 3 slices of cheese ,reg chips & sugar free buddy", RestaurantId = 4, Price = 90, PrepTime = DateTime.Parse("00:30:00") },
                new Food() { FoodId = 11, FoodName = "1/4 Chicken", Description = "1/4 Chicken, bun and sugar free can", RestaurantId = 4, Price = 53, PrepTime = DateTime.Parse("00:30:00") },
                new Food() { FoodId = 12, FoodName = "BratPack", Description = "Snack Beefburger, reg chips & ceres 200ml", RestaurantId = 4, Price = 51, PrepTime = DateTime.Parse("00:20:00") },

                new Food() { FoodId = 13, FoodName = "Chicken & Mushroom Pizza", Description = "Chicken, mushroom, tomato, onion, Debonairs sauce", RestaurantId = 4, Price = 100, PrepTime = DateTime.Parse("00:35:00") },
                new Food() { FoodId = 14, FoodName = "Something Meaty", Description = "Ham, pepperoni, bacon, ground beef, BBQ sauce", RestaurantId = 4, Price = 105, PrepTime = DateTime.Parse("00:40:00") },
                new Food() { FoodId = 15, FoodName = "Original Veggie", Description = "Mushroom, olives, pineapple, onion, tomato, spring onion", RestaurantId = 4, Price = 86, PrepTime = DateTime.Parse("00:30:00") }
                );

            context.Drivers.AddOrUpdate(
                 a => a.DriverId,
                 new Driver() { DriverId = 1, FirstName = "John", LastName = "Gordon" },
                 new Driver() { DriverId = 2, FirstName = "Casper", LastName = "Irwin" },
                 new Driver() { DriverId = 3, FirstName = "Tyrone", LastName = "Simon" }
                 );

            context.Customers.AddOrUpdate(
                a => a.CustomerId,
                new Customer() { CustomerId = 1, FirstName = "Clayton", LastName = "Horner" },
                new Customer() { CustomerId = 2, FirstName = "Ashton", LastName = "Fleming" },
                new Customer() { CustomerId = 3, FirstName = "Bentley", LastName = "Healy" },
                new Customer() { CustomerId = 4, FirstName = "Sonnie", LastName = "Hartley" }
                );

            context.Order.AddOrUpdate(
                a => a.OrderId,
                new Orders() { OrderId = 1, CustomerId = 1, DriverId = 1, DriverAcceptance = true, RestaurantAcceptance = true, DeliveryTime = DateTime.Parse("00:25:00"), OrderStatus = "Not Collected Yet", DeliveryFee = 10, OverallFee = 50, PickupTime = DateTime.Parse("00:25:00"), CustomerAddress = "169 Upper Canterbury St, Gardens, Cape Town" },
                new Orders() { OrderId = 2, CustomerId = 2, DriverId = null, DriverAcceptance = false,  RestaurantAcceptance = false, DeliveryTime = DateTime.Parse("00:00:00"), OrderStatus = "Pending...", DeliveryFee = 15, OverallFee = 55, PickupTime = DateTime.Parse("00:00:00"), CustomerAddress = "Grove Street", RestaurantId = 2}
                );
            context.FoodOrders.AddOrUpdate(
                a=>a.FoodOrderId,
                new FoodOrder() { FoodOrderId = 1, OrderId = 1, FoodId = 2}
                );
        }
    }
}
