using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetFood_API.Classes;

namespace GetFood_API.Models
{
    public class NewOrderResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerAddress { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }

        public NewOrderResponse(Orders orderInfo)
        {
            FirstName = orderInfo.Customer.FirstName;
            LastName = orderInfo.Customer.LastName;
            CustomerAddress = orderInfo.CustomerAddress;
            RestaurantName = orderInfo.Restaurant.RestaurantName;
            RestaurantAddress = orderInfo.Restaurant.Address;
        }
    }
}
