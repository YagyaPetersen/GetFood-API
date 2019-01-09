using GetFood_API.Classes;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GetFood_API.Models;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        //Order_________________________________________________________________________________________________________________
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
            newOrder.OrderStatus = "Pending...";

            newOrder.RestaurantId = orderInfo.RestaurantId;
            newOrder.Restaurant = allRestaurants;

            newOrder.PickupTime = 0;
            newOrder.DeliveryTime = 0;

            GetFoodContext.Order.Add(newOrder);
            GetFoodContext.SaveChanges();

            var NewOrderResponse = new NewOrderResponse(newOrder);

            return Json(NewOrderResponse);
        }

       
    }
}
