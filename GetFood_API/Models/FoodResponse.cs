using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GetFood_API.Classes;

namespace GetFood_API.Models
{
    public class FoodResponse
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public string Restaurant { get; set; }
        public decimal Price { get; set; }
        public DateTime PrepTime { get; set; }

        public FoodResponse(Food foodInfo)
        {
            FoodId = foodInfo.FoodId;
            FoodName = foodInfo.FoodName;
            Description = foodInfo.Description;
            Restaurant = foodInfo.Restaurant.RestaurantName;
            Price = foodInfo.Price;
            PrepTime = foodInfo.PrepTime;
        }
    }
}