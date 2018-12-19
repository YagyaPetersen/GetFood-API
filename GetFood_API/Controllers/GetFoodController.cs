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

            newOrder.RestaurantId = orderInfo.RestaurantId;
            newOrder.Restaurant = allRestaurants;

            newOrder.PickupTime = DateTime.Parse("00:00:00");
            newOrder.DeliveryTime = DateTime.Parse("00:00:00");

            GetFoodContext.Order.Add(newOrder);
            //GetFoodContext.SaveChanges();

            var NewOrderResponse = new NewOrderResponse(newOrder);

            return Json(NewOrderResponse);
        }

        //GetOrder_____________________________________________________________________________________________________________
        [Route("api/GetOrder/{id}")]
        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            var all = GetFoodContext.Order
                .Include(a => a.Foods)
                .Include(a => a.Customer)
                .Where(a => a.OrderId == id)
                .ToList()
                .FirstOrDefault();

            return Json(all);
        }
    }
}
