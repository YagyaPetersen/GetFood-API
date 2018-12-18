using GetFood_API.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

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
            else if (order.RestaurantAcceptance == true)
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

        //Cart_________________________________________________________________________________________________________________
        [Route("api/Cart")]
        [HttpPatch]
        public IHttpActionResult OrderFood([FromBody]FoodOrder data)
        {
            FoodOrder foodOrder = new FoodOrder();

            int[] foods = data.Foods;
            ICollection<object> foodStorage = new List<object>();

            var currentOrder = GetFoodContext.Order.Where(a => a.OrderId == data.OrderId).ToList().FirstOrDefault();
            foodOrder.Orders = currentOrder;

            foreach (var i in foods)
            {
                var selectedFood = GetFoodContext.Foods.Where(a => a.FoodId == i).ToList().FirstOrDefault();
                if (selectedFood.RestaurantId != currentOrder.RestaurantId)
                {
                    foodStorage.Add(selectedFood);

                }
                else
                {
                    return Json(foods);
                }
                
            }

            currentOrder.Foods = foodStorage;
            
            GetFoodContext.Order.Add(currentOrder);
            //GetFoodContext.SaveChanges();
            return Json(foodOrder);
        }

        [Route("api/FinaliseOrder")]
        [HttpPatch]
        public IHttpActionResult Finalize([FromBody]Orders orderInfo)
        {
            //foreach (var i in orderInfo.Foods)
            //{
            var fee = GetFoodContext.Order.Where(a => a.OrderId == orderInfo.OrderId).ToList().FirstOrDefault();

            foreach (var i in fee.Foods)
            {
                var qwe = fee.Foods.Contains("Price");
                return Json(qwe);
            }

            
            return Json(fee);
        }

    }
}
