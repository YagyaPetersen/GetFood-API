using System;
using GetFood_API.Classes;
using System.Data.Entity;
using System.Linq;
using System.Net.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [System.Web.Http.Route("api/restaurant/{id}/food")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAll(int id)
        {
            var all = GetFoodContext.Foods.Include(a => a.Restaurant)
                .Where(a => a.RestaurantId == id)
                .ToList();

            return Ok(all);
        }


        [System.Web.Http.Route("api/order")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostOrder([FromBody] FoodOrder orderInfo)
        {
            FoodOrder foodOrders = new FoodOrder();

//==============Food=========================================================
            var allFoods = GetFoodContext.Foods.Include(a => a.Restaurant).Where(a => a.FoodId == orderInfo.FoodId).ToList().FirstOrDefault();
            allFoods.FoodId = orderInfo.FoodId;
            foodOrders.FoodId = orderInfo.FoodId;
            foodOrders.Food = allFoods;

//===============Order=======================================================
            Orders orders = new Orders();
            var storedOrders = GetFoodContext.Order.Include(a => a.Customer).Where(a => a.OrderId == orderInfo.OrderId).ToList().FirstOrDefault();
            orders.OrderId = orderInfo.OrderId;
            foodOrders.Orders = orders;
            foodOrders.OrderId = orders.OrderId;

//==============Customer======================================================
            var allCustomers = GetFoodContext.Customers.Where(a => a.CustomerId == orderInfo.Orders.CustomerId).ToList().FirstOrDefault();
            orders.CustomerId = allCustomers.CustomerId;
            orders.CustomerAddress = orderInfo.Orders.CustomerAddress;
            orders.Customer = allCustomers;

//==============Order Properties==============================================
            foodOrders.Orders.DriverAcceptance = orderInfo.Orders.DriverAcceptance;
            foodOrders.Orders.RestaurantAcceptance = orderInfo.Orders.RestaurantAcceptance;
            foodOrders.Orders.OverallFee = allFoods.Price + foodOrders.Orders.DeliveryFee;

            if (orders.RestaurantAcceptance == false || orders.DriverAcceptance == false)
            {
                foodOrders.Orders.OrderStatus = "Pending...";
            }
            else
            {
                foodOrders.Orders.OrderStatus = "Order Confirmed";
            }

            var drivers = GetFoodContext.Drivers

//==============Saving=========================================================
            GetFoodContext.FoodOrders.Add(foodOrders);
            //GetFoodContext.SaveChanges();
            return Json(foodOrders);
        }




        [System.Web.Http.Route("api/deleteOrder/{id}")]
        [System.Web.Http.HttpDelete]
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
