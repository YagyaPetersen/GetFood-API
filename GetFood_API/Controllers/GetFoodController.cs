using GetFood_API.Classes;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult PostOrder([FromBody] FoodOrder orderInfo)
        {
            FoodOrder foodOrders = new FoodOrder();

//==============Food=========================================================

            var allFoods = GetFoodContext.Foods
                .Include(a => a.Restaurant)
                .Where(a => a.FoodId == orderInfo.FoodId)
                .ToList()
                .FirstOrDefault();

            allFoods.FoodId = orderInfo.FoodId;
            foodOrders.FoodId = orderInfo.FoodId;
            foodOrders.Food = allFoods;

//===============Order=======================================================

            Orders orders = new Orders();
            var storedOrders = GetFoodContext.Order
                .Include(a => a.Customer)
                .Where(a => a.OrderId == orderInfo.OrderId)
                .ToList()
                .FirstOrDefault();

            orders.OrderId = orderInfo.OrderId;
            foodOrders.Orders = orders;
            foodOrders.OrderId = orders.OrderId;

//==============Customer======================================================

            var allCustomers = GetFoodContext.Customers
                .Where(a => a.CustomerId == orderInfo.Orders.CustomerId)
                .ToList()
                .FirstOrDefault();

            var allDrivers = GetFoodContext.Drivers
                .Where(a => a.DriverId == orderInfo.Orders.DriverId)
                .ToList()
                .FirstOrDefault();

            orders.CustomerId = allCustomers.CustomerId;
            orders.CustomerAddress = orderInfo.Orders.CustomerAddress;
            orders.Customer = allCustomers;
            orders.Driver = 


            foodOrders.Orders.PickupTime = orderInfo.Orders.PickupTime;
            foodOrders.Orders.DeliveryTime = orderInfo.Orders.DeliveryTime;

            if (orders.DriverAcceptance == false || orders.RestaurantAcceptance == false)
            {
                orders.OrderStatus = "Pending...";
            }
            else
            {
                orders.OrderStatus = "Order Confirmed";
            }

            orders.OverallFee = foodOrders.Food.Price;
            //==============Saving=========================================================

            GetFoodContext.FoodOrders.Add(foodOrders);
           // GetFoodContext.SaveChanges();
            return Json(foodOrders);
        }
//=============================================================================

        
        [Route("api/foodOrder/{id}")]
        [HttpPatch]
        public IHttpActionResult PatchAcceptance(int id, [FromBody]OrderRequest request)
        {
            var orders = GetFoodContext.FoodOrders
                .Include(a => a.Orders)
                .Include(a=>a.Food)
                .Include(a=>a.Orders.Customer)
                .Include(a=>a.Orders.Driver)
                .Include(a=>a.Food.Restaurant)
               // .Where(a => a.FoodOrderId == id)
                .FirstOrDefault(a => a.FoodOrderId == id);

            orders.Orders.RestaurantAcceptance = request.RAccept;
            orders.Orders.DriverAcceptance = request.DAccept;
              
            if (request.DAccept == false || request.RAccept == false)
            {
                orders.Orders.OrderStatus = "Pending...";
            }
            //else if (request.DAccept == true)
            //{
            //    orders.Orders.OrderStatus = "Restaurant Pending...";
            //    var drivers = GetFoodContext.Drivers
            //        .Where(a => a.DriverId == orders.Orders.DriverId)
            //        .ToList()
            //        .FirstOrDefault();
            //    orders.Orders.DeliveryFee = request.details.Orders.DeliveryFee;
            //    orders.Orders.DriverId = drivers.DriverId;
            //    orders.Orders.Driver = drivers;
            //    orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;
            //}
            //else if (request.RAccept == true)
            //{
            //    orders.Orders.OrderStatus = "Driver Pending...";
            //    orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;
            //    orders.Orders.PickupTime = orders.Food.PrepTime;
            //}
            else
            {
                orders.Orders.OrderStatus = "Order Confirmed";
                var drivers = GetFoodContext.Drivers
                    .Where(a => a.DriverId == orders.Orders.DriverId)
                    .ToList()
                    .FirstOrDefault();

                orders.Orders.DeliveryFee = request.details.Orders.DeliveryFee;
                orders.Orders.DriverId = drivers.DriverId;
                orders.Orders.Driver = drivers;
                orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;

                orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;
                orders.Orders.PickupTime = orders.Food.PrepTime;
            }

            return Ok(orders);
        }

        [Route("api/deleteOrder/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var all = GetFoodContext.FoodOrders
                .Where(a => a.FoodOrderId == id)
                .FirstOrDefault();

            GetFoodContext.FoodOrders.Remove(all);
            GetFoodContext.SaveChanges();

            return Json(GetFoodContext.FoodOrders);
        }
    }
}
