using GetFood_API.Classes;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GetFood_API.Models.Response;
using Microsoft.Ajax.Utilities;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/Restaurant/{restaurantID}")]
        [HttpGet]
        public IHttpActionResult GetFood(int restaurantID)
        {
            var foods = GetFoodContext.Foods
                .Include(a => a.Restaurant)
                .Where(a => a.RestaurantId == restaurantID)
                .ToList();

            return Json(foods);
        }

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
            GetFoodContext.SaveChanges();

                //var PostResponse = new PostResponse(foodOrders.Orders.Customer.FirstName, foodOrders.Orders.Customer.LastName, foodOrders.Orders.Driver.FirstName, foodOrders.Orders.Driver.LastName, foodOrders.Orders.PickupTime, foodOrders.Orders.OrderStatus, foodOrders.Orders.DeliveryFee, foodOrders.Orders.OverallFee, foodOrders.Orders.DeliveryTime, foodOrders.Orders.CustomerAddress, foodOrders.Food.Restaurant.RestaurantName, foodOrders.Food.Restaurant.Address, foodOrders.Food.FoodName, foodOrders.Food.Description, foodOrders.Food.Price, foodOrders.Food.PrepTime);

            return Json(foodOrders);
        }
//=============================================================================
        

        [Route("api/foodOrder")]
        [HttpPatch]
        public IHttpActionResult PatchAcceptance([FromBody] OrderRequest request)
        {
            var orders = GetFoodContext.FoodOrders
                .Include(a => a.Orders)
                .Include(a=>a.Food)
                .Include(a=>a.Orders.Customer)
                .Include(a=>a.Orders.Driver)
                .Include(a=>a.Food.Restaurant).ToList()
                .FirstOrDefault(a => a.FoodOrderId == request.RequestId);

            orders.Orders.RestaurantAcceptance = request.RestaurantAcceptance;
            orders.Orders.DriverAcceptance = request.DriverAcceptance;
              
            if (request.DriverAcceptance == false || request.RestaurantAcceptance == false)
            {
                orders.Orders.OrderStatus = "Pending...";
            }
            else
            {
                orders.Orders.OrderStatus = "Order Confirmed";
                var drivers = GetFoodContext.Drivers
                    .Where(a => a.DriverId == request.DriverId)
                    .ToList()
                    .FirstOrDefault();

                orders.Orders.DeliveryFee = request.DeliveryFee;
                orders.Orders.DriverId = request.DriverId;
                orders.Orders.Driver = drivers;
                orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;

                orders.Orders.OverallFee = orders.Food.Price + orders.Orders.DeliveryFee;
                orders.Orders.PickupTime = orders.Food.PrepTime;
            }

            GetFoodContext.FoodOrders.Add(orders);
            GetFoodContext.SaveChanges();

         

            return Json(orders);
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
