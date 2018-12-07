using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetFood_API.Classes;

namespace GetFood_API.Models.Response
{
    public class PostResponse
    {
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

        public PostResponse(string a, string b, string c, string d, DateTime e, string f, decimal g, decimal h, DateTime i, string j, string k, string l, string m, string n, decimal o, DateTime p)
        {
            CustomerFName = a;//post.FirstOrDefault().Orders.Customer.FirstName;
            CustomerLName = b;//post.FirstOrDefault().Orders.Customer.LastName;
            DriverFName = c;//post.FirstOrDefault().Orders.Driver.FirstName;
            DriverLName = d; //post.FirstOrDefault().Orders.Driver.FirstName;
            PickUpTime = e;//post.FirstOrDefault().Orders.PickupTime;
            OrderStatus = f;//post.FirstOrDefault().Orders.OrderStatus;
            DeliveryFee = g;//post.FirstOrDefault().Orders.DeliveryFee;
            OverallFee = h;//post.FirstOrDefault().Orders.OverallFee;
            DeliveryTime = i;//post.FirstOrDefault().Orders.DeliveryTime;
            Address = j;//post.FirstOrDefault().Orders.CustomerAddress;
            Restaurant = k;//post.FirstOrDefault().Food.Restaurant.RestaurantName;
            RestaurantAddress = l;//post.FirstOrDefault().Food.Restaurant.Address;
            Food = m;//post.FirstOrDefault().Food.FoodName;
            Description = n;//post.FirstOrDefault().Food.Description;
            Price = o;//post.FirstOrDefault().Food.Price;
            PrepTime = p; //post.FirstOrDefault().Food.PrepTime;
        }
    }
}