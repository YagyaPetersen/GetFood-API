using GetFood_API.Classes;
using System.Data.Entity;

namespace GetFood_API
{
    public class GetFoodContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Food> Foods { get; set; }
        //public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<Orders> Order { get; set; }

        public GetFoodContext()
            : base("name=GetFoodDatabase")
        {

        }
    }
}