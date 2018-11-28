using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetFood_API.Controllers
{
    public class GetFoodController : ApiController
    {
       GetFoodContext GetFoodContext = new GetFoodContext();

        [Route("api/customer")]
        [HttpGet]
        public IHttpActionResult GetAll(/*int customerId, int driverId*/)
        {
            using (var newContext = new GetFoodContext())
            {

                var all = GetFoodContext.Order.Include(a=>a.Customer).Include(a=>a.Driver)/*.Include(a=>a.Food).Include(a=>a.Orders).Include(a=>a.Orders.Customer).Include(a=>a.Orders.Driver).Include(a=>a.Food.Restaurant)*/
                    //.Where(a=>a.RestaurantId == customerId && a.FoodId == driverId )
                    .ToList();

                return Ok(all);
            }
        }











    }
}
