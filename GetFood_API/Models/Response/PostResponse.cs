using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetFood_API.Classes;

namespace GetFood_API.Models.Response
{
    public class PostResponse
    {
        private List<FoodOrder> post;

        public string CustomerFName { get; set; }
        public string CustomerLName { get; set; }
        public string DriverFName { get; set; }
        public string DriverLName { get; set; }
        public DateTime PickUpTime { get; set; }
        public string OrderStatus { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal OverallFee { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Address { get; set; }
        public string Restaurant { get; set; }
        public string RestaurantAddress { get; set; }
        public string Food { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PrepTime { get; set; }

        public PostResponse(List<FoodOrder> post/*string a, string b, string c, string d, DateTime e, string f, decimal g, decimal h, DateTime i, string j, string k, string l, string m, string n, decimal o, DateTime p*/)
        {
            CustomerFName = post.FirstOrDefault().Orders.Customer.FirstName;
            CustomerLName = post.FirstOrDefault().Orders.Customer.LastName;
            DriverFName = post.FirstOrDefault().Orders.Driver.FirstName;
            DriverLName =  post.FirstOrDefault().Orders.Driver.FirstName;
            PickUpTime = post.FirstOrDefault().Orders.PickupTime;
            OrderStatus = post.FirstOrDefault().Orders.OrderStatus;
            DeliveryFee = post.FirstOrDefault().Orders.DeliveryFee;
            OverallFee = post.FirstOrDefault().Orders.OverallFee;
            DeliveryTime = post.FirstOrDefault().Orders.DeliveryTime;
            Address = post.FirstOrDefault().Orders.CustomerAddress;
            Restaurant = post.FirstOrDefault().Food.Restaurant.RestaurantName;
            RestaurantAddress = post.FirstOrDefault().Food.Restaurant.Address;
            Food =post.FirstOrDefault().Food.FoodName;
            Description = post.FirstOrDefault().Food.Description;
            Price = post.FirstOrDefault().Food.Price;
            PrepTime = post.FirstOrDefault().Food.PrepTime;
        }
    }
}