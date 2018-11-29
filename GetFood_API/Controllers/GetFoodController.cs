using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web.Http;
using GetFood_API.Classes;
using GetFood_API.Models.Response;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
        GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/restaurant/{id}/food")]
        [HttpGet]
        public IHttpActionResult GetAll(int id)
        {
           

                var all = GetFoodContext.Foods.Include(a => a.Restaurant)
                    .Where(a => a.RestaurantId == id)
                    .ToList();

                return Ok(all);
            
        }

        [Route("api/order")]
        [HttpPost]
        public IHttpActionResult PostOrder([FromBody]Customer CustomerInfo)
        {
           Customer customer = new Customer();
            customer.FirstName = CustomerInfo.FirstName;
            customer.LastName = CustomerInfo.LastName;
            var a = GetFoodContext.Customers.Add(customer);
            return Json(a);
            
        }

        [Route("api/customers")]
        [HttpGet]
        public IHttpActionResult GetCus()
        {
                var all = GetFoodContext.Customers
                    //.Where(a => a.RestaurantId == id)
                    .ToList();

                return Ok(all);
            
        }
    }
}
