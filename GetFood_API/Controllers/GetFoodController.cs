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
        public IHttpActionResult PostOrder([FromBody]FoodOrder orderInfo)
        {
            FoodOrder foodOrders = new FoodOrder();

            var allFoods = GetFoodContext.Foods.Include(a => a.Restaurant).Where(a => a.FoodId == orderInfo.FoodId).ToList().FirstOrDefault();
            allFoods.FoodId = orderInfo.FoodId;
            foodOrders.FoodId = orderInfo.FoodId;
            foodOrders.Food = allFoods;

            Orders orders = new Orders();
            //var orders = GetFoodContext.Order.Include(a => a.Customer).Where(a => a.OrderId == orderInfo.OrderId).ToList().FirstOrDefault();
            orders.OrderId = orderInfo.OrderId;
            foodOrders.Orders = orders;
            foodOrders.OrderId = orders.OrderId;
            
            //orders.Orders.CustomerAddress = orderInfo.Orders.CustomerAddress;

            //if (orders.DriverAcceptance = true)
            //{
            //    orders.DriverId = orders.DriverId;
            //    orders.Driver = orderInfo.Orders.Driver;
            //}
            //else 
            //    {
            //        orders.Driver = null;
            //        orders.DriverId = 0;
            //    }

            //GetFoodContext.Customers.Add(customer);
            //GetFoodContext.SaveChanges();
            return Json(foodOrders);
        }

        [System.Web.Http.Route("api/deleteOrder/{id}")]
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var all = GetFoodContext.Order
                .Where(a => a.OrderId == id)
                .FirstOrDefault();

            GetFoodContext.Order.Remove(all);
            GetFoodContext.SaveChanges();

            return Json(GetFoodContext.Order);
        }
    }
}
