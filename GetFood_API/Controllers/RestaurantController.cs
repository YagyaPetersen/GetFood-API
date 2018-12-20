using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetFood_API.Models;

namespace GetFood_API.Controllers
{
    public class RestaurantController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/restaurants")]
        [HttpGet]
        public IHttpActionResult GetRestaurants()
        {
            var restaurant = GetFoodContext.Restaurants
                .ToList();
            
            return Json(restaurant);
        }

        [Route("api/restaurants/{restaurantId}/food")]
        [HttpGet]
        public IHttpActionResult GetFoods(int restaurantId)
        {
            var foods = GetFoodContext.Foods
                .Where(a => a.RestaurantId == restaurantId)
                .ToList();

            List<object> newFoodResponse = new List<object>();

            foreach (var food in foods)
            {
                var FoodResponse = new FoodResponse(food);   
                newFoodResponse.Add(FoodResponse);
            }

           return Json(newFoodResponse);
        }

    }
}
