using GetFood_API.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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

            GetFoodContext.Order.Add(newOrder);
            //GetFoodContext.SaveChanges();

            return Json(newOrder);
        }

//Delete_______________________________________________________________________________________________________________
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
    }
}
