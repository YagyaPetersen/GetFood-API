using GetFood_API.Classes;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GetFood_API.Models;

namespace GetFood_API.Controllers
{
    public class PatchController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        //Restaurant___________________________________________________________________________________________________________
        [Route("api/acceptRestaurant")]
        [HttpPatch]
        public IHttpActionResult AcceptRestaurant([FromBody]OrderRequest request)
        {
            var order = GetFoodContext.Order
                .Include(a=>a.Customer)
                .Include(a=>a.Foods)
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

            var RestaurantResponse = new RestaurantResponse(order);

            GetFoodContext.SaveChanges();

            return Json(RestaurantResponse);
        }

        //Driver_______________________________________________________________________________________________________________
        [Route("api/acceptDriver")]
        [HttpPatch]
        public IHttpActionResult AcceptDriver([FromBody] OrderRequest request)
        {
            var order = GetFoodContext.Order
                .Include(a=>a.Customer)
                .Include(a=>a.Foods)
                .ToList()
                .FirstOrDefault(a => a.OrderId == request.RequestId);

            var drivers = GetFoodContext.Drivers
                .ToList()
                .FirstOrDefault(a=>a.DriverId == request.DriverId);

            if (order.RestaurantAcceptance == false)
            {
                return NotFound();
            }
            else if (order.RestaurantAcceptance == true)
            {
                order.DriverAcceptance = request.DriverAcceptance;

                if (order.DriverAcceptance == true)
                {
                    order.OrderStatus = "Order Confirmed";
                    order.DeliveryFee = request.DeliveryFee;
                    order.OverallFee = order.DeliveryFee + order.OverallFee;
                    order.DriverId = request.DriverId;
                    order.Driver = drivers;
                    order.DeliveryTime = request.DeliveryTime;
                }
                else
                {
                    return NotFound();
                }
                GetFoodContext.SaveChanges();
            }
            var DriverResponse = new DriverResponse(order);

            return Json(DriverResponse);
        }

        //Cart_________________________________________________________________________________________________________________
        [Route("api/Cart")]
        [HttpPatch]
        public IHttpActionResult OrderFood([FromBody]FoodOrder data)
        {
            FoodOrder foodOrder = new FoodOrder();
            int[] foods = data.Foods;

            ICollection<Food> foodStorage = new List<Food>();
            ICollection<decimal> TotalFee = new List<decimal>();

            var currentOrder = GetFoodContext.Order.Include(a=>a.Customer).Where(a => a.OrderId == data.OrderId).ToList().FirstOrDefault();
            foodOrder.Orders = currentOrder;

            foreach (var i in foods)
            {
                var selectedFood = GetFoodContext.Foods.Where(a => a.FoodId == i).ToList().FirstOrDefault();
                TotalFee.Add(selectedFood.Price);

                if (currentOrder.RestaurantId == selectedFood.RestaurantId)
                {
                    foodStorage.Add(selectedFood);
                }
                else
                {
                    return NotFound();
                }
            }

            currentOrder.Foods = foodStorage.ToArray();
            currentOrder.OverallFee = TotalFee.Sum() + currentOrder.DeliveryFee;

            GetFoodContext.SaveChanges();

            return Ok();
        }
    }
}
