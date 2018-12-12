using System;
using System.Collections.Generic;
using GetFood_API.Classes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult PostOrder([FromBody] Orders orderInfo)
        {
            Orders newOrder = new Orders();

            newOrder.OrderId = orderInfo.OrderId;
            newOrder = orderInfo;

            var allCustomers = GetFoodContext.Customers
                .Where(a => a.CustomerId == orderInfo.CustomerId)
                .ToList()
                .FirstOrDefault();

            var allRestaurants = GetFoodContext.Restaurants
                .Where(a => a.RestaurantId == orderInfo.RestaurantId)
                .ToList()
                .FirstOrDefault();

            newOrder.CustomerId = allCustomers.CustomerId;
            newOrder.CustomerAddress = orderInfo.CustomerAddress;
            newOrder.Customer = allCustomers;

            newOrder.RestaurantId = orderInfo.RestaurantId;
            newOrder.Restaurant = allRestaurants;
            //orders.OverallFee = newOrder.Food.Price;

             GetFoodContext.Order.Add(newOrder);
            //GetFoodContext.SaveChanges();

            return Json(newOrder);
        }

        [Route("api/Cart")]
        [HttpPost]
        public IHttpActionResult OrderFood([FromBody]FoodOrder data)
        {
            FoodOrder foodOrder = new FoodOrder();
            var orders = GetFoodContext.Order
                .ToList()
                .FirstOrDefault();
            var food = GetFoodContext.Foods
                .ToList()
                .FirstOrDefault();
            foodOrder.OrderId = data.OrderId;
            foodOrder.Orders = orders;
            foodOrder.FoodId = data.FoodId;
            foodOrder.Food = food;
            return Json(foodOrder);
        }

        [Route("api/deleteOrder/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var all = GetFoodContext.Order
                .Where(a => a.OrderId == id)
                .ToList()
                .FirstOrDefault();

            GetFoodContext.Order.Remove(all);
            GetFoodContext.SaveChanges();

            return Json(GetFoodContext.Order);
        }

        [Route("api/test")]
        [HttpGet]
        public IHttpActionResult GetCart()
        {
            //id = 1;
            //var food = GetFoodContext.Order.ToList().FirstOrDefault();
            //Cart cart = new Cart();
            //Orders food = new Orders();
            int[] foods = new int[] {2,2};
            //info.FoodId = [1,2];
            //var we = GetFoodContext.Foods.ToList().FirstOrDefault(a => a.FoodId == foods[1]);
            //cart.Foods = food;

            foreach (int i in foods)
            {
                var qw = GetFoodContext.Foods.Where(a => i == a.FoodId).ToList();

                return Json(qw);
            }
            // cart.Foods.Add(food);
           
             
          
            return Ok();
        }
    }
}
