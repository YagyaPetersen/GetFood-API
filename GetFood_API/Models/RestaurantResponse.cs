using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetFood_API.Classes;

namespace GetFood_API.Models
{
    public class RestaurantResponse
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public string Restaurant { get; set; }
        public List<Food> Foods { get; set; }
        public class Food
        {
            public string FoodName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public DateTime PrepTime { get; set; }
        }
        List<Food> ChosenFood = new List<Food>();
        public bool DriverAcceptance { get; set; }
        public bool RestaurantAcceptance { get; set; }
        public string OrderStatus { get; set; }
        public decimal OverallFee { get; set; }
        public DateTime PickupTime { get; set; }

        public RestaurantResponse(Orders orderInfo)
        {
            OrderId = orderInfo.OrderId;
            Customer = orderInfo.Customer.FirstName + " " + orderInfo.Customer.LastName;
            CustomerAddress = orderInfo.CustomerAddress;
            Restaurant = orderInfo.Restaurant.RestaurantName;
            ICollection<Food> ChosenFood = new List<Food>();

            foreach (var i in orderInfo.Foods)
            {
                var food = new Food()
                {
                    FoodName = i.FoodName,
                    Description = i.Description,
                    Price = i.Price,
                    PrepTime = i.PrepTime
                };
                ChosenFood.Add(food);
            }
            Foods = ChosenFood.ToList();
            DriverAcceptance = orderInfo.DriverAcceptance;
            RestaurantAcceptance = orderInfo.RestaurantAcceptance;
            OrderStatus = orderInfo.OrderStatus;
            OverallFee = orderInfo.OverallFee;
            PickupTime = orderInfo.PickupTime;
        }
    }
}