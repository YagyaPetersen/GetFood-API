using GetFood_API.Classes;
using GetFood_API.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GetFood_API.Controllers
{

    public class PatchController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        public List<object> test = new List<object>();

        //Restaurant___________________________________________________________________________________________________________
        [Route("api/acceptRestaurant")]
        [HttpPatch]
        public IHttpActionResult AcceptRestaurant([FromBody]OrderRequest request)
        {
            var order = GetFoodContext.Order
                .Include(a=>a.Customer)
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
            var foods = data.FoodNumbers;

            ICollection<Food> foodStore = new List<Food>();
            ICollection<decimal> TotalFee = new List<decimal>();

                var currentOrder = GetFoodContext.Order.Include(a => a.Customer).Where(a => a.OrderId == data.OrderId)
                    .FirstOrDefault();

                currentOrder.Foods = new List<object>();

                foreach (var i in foods)
                {
                    var selectedFood = GetFoodContext.Foods.Where(a => a.FoodId == i).ToList().FirstOrDefault();
                    TotalFee.Add(selectedFood.Price);

                    if (currentOrder.RestaurantId == selectedFood.RestaurantId)
                    {
                    
                    currentOrder.Foods.Add(selectedFood);
                        test.Add(selectedFood.FoodName);
                        GetFoodContext.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                //test = currentOrder.Foods;
                //currentOrder.Foods = new List<object>();
                //currentOrder.Foods = foodStore.ToArray();
                currentOrder.OverallFee = TotalFee.Sum() + currentOrder.DeliveryFee;

                GetFoodContext.SaveChanges();

                return Json(currentOrder);
            }


        //GetOrder_____________________________________________________________________________________________________________
        [Route("api/GetOrder/{id}")]
        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        { 
            var all = GetFoodContext.Order
                .Include(a => a.Customer)
                //.Include(a=>a.Foods)
                .Where(a => a.OrderId == id)
                .ToList()
                .FirstOrDefault();

            all.Foods = test;

            var FinalResponse = new FinalResponse(all);

            return Json(all);
        }
    }
}
