using System;
using GetFood_API.Classes;
using System.Data.Entity;
using System.Linq;
using System.Net.Configuration;
using System.Web.Http;
using System.Web.Mvc;

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
        public IHttpActionResult PostOrder([FromBody]Orders orderInfo)
        {
            Orders orders = new Orders();
           
            var customer = GetFoodContext.Customers.Where(a => a.CustomerId == orderInfo.CustomerId).ToList().FirstOrDefault();
            var lastName = customer.LastName;
           // orders.Customer = customer;
            //orders.CustomerId = orderInfo.CustomerId;
           // orders.DriverId = orderInfo.DriverId;
            orders.Customer = orderInfo.Customer;
            orders.CustomerAddress = orderInfo.CustomerAddress;

            if (orders.DriverAcceptance = true)
            {
                orders.DriverId = orderInfo.DriverId;
                orders.Driver = orderInfo.Driver;
            }
            else 
                {
                    orders.Driver = null;
                    orders.DriverId = 0;
                }

            //Customer customer = new Customer();
            //customer.FirstName = CustomerInfo.FirstName;
            //customer.LastName = CustomerInfo.LastName;
            //GetFoodContext.Customers.Add(customer);
            //GetFoodContext.SaveChanges();
            return Json(orderInfo);
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
