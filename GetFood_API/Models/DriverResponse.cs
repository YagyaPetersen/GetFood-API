using GetFood_API.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace GetFood_API.Models
{
    public class DriverResponse
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public string Restaurant { get; set; }
        public string Driver { get; set; }
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
        public decimal DeliveryFee { get; set; }
        public decimal OverallFee { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DeliveryTime { get; set; }

        public DriverResponse(Orders orderInfo)
        {
            OrderId = orderInfo.OrderId;
            Customer = orderInfo.Customer.FirstName + " " + orderInfo.Customer.LastName;
            CustomerAddress = orderInfo.CustomerAddress;
            Restaurant = orderInfo.Restaurant.RestaurantName;
            Driver = orderInfo.Driver.FirstName + " " + orderInfo.Driver.LastName;
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
            DeliveryFee = orderInfo.DeliveryFee;
            OverallFee = orderInfo.OverallFee;
            PickupTime = orderInfo.PickupTime;
            DeliveryTime = orderInfo.DeliveryTime;
        }
    }
}