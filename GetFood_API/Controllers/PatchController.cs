using GetFood_API.Classes;
using System.Linq;
using System.Web.Http;

namespace GetFood_API.Controllers
{
    public class PatchController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/acceptRestaurant")]
        [HttpPatch]
        public IHttpActionResult AcceptRestaurant([FromBody]OrderRequest request)
        {
            var order = GetFoodContext.Order
                .ToList()
                .FirstOrDefault(a => a.OrderId == request.RequestId);

            order.RestaurantAcceptance = request.RestaurantAcceptance;
            order.PickupTime = request.PickupTime;

            if (order.RestaurantAcceptance == true)
            {
                order.OrderStatus = "Driver Pending...";
            }
            else
            {
                order.OrderStatus = "Pending...";
            }

            GetFoodContext.Order.Add(order);
           // GetFoodContext.SaveChanges();

            return Json(order);
        }

        [Route("api/acceptDriver")]
        [HttpPatch]
        public IHttpActionResult AcceptDriver([FromBody] OrderRequest request)
        {
            var order = GetFoodContext.Order
                .ToList()
                .FirstOrDefault(a => a.OrderId == request.RequestId);

            var drivers = GetFoodContext.Drivers
                .ToList()
                .FirstOrDefault();

            if (order.RestaurantAcceptance == false)
            {
                return NotFound();
            }
            else if(order.RestaurantAcceptance == true)
            {
                order.DriverAcceptance = request.DriverAcceptance;

                if (order.DriverAcceptance == true)
                {
                    order.OrderStatus = "Order Confirmed";
                    order.DeliveryFee = request.DeliveryFee;
                    order.DriverId = request.DriverId;
                    order.Driver = drivers;
                    order.DeliveryTime = request.DeliveryTime;
                }
                else
                {
                    return NotFound();
                }

                GetFoodContext.Order.Add(order);
                //GetFoodContext.SaveChanges();
            }

            return Json(order);
        }
    }
}
