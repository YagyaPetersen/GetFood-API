using GetFood_API.Classes;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

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
        public IHttpActionResult PostOrder([FromBody]Orders orderInfo)
        {
           Orders orders = new Orders();
            orders.CustomerId = orderInfo.CustomerId;
           // orders.DriverId = orderInfo.DriverId;
            orders.CustomerAddress = orders.CustomerAddress;

            if (orders.DriverAcceptance = true)
            {
                orders.DriverId = orderInfo.DriverId;
                orders.Driver = orders.Driver
            


            else
                {
                    orders.Driver = null;
                }
            }



            //Customer customer = new Customer();
            //customer.FirstName = CustomerInfo.FirstName;
            //customer.LastName = CustomerInfo.LastName;
            //GetFoodContext.Customers.Add(customer);
            //GetFoodContext.SaveChanges();
            return Json(orderInfo);
        }

        [Route("api/deleteCustomer/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteCus(int id)
        {
            var all = GetFoodContext.Customers
                .Where(a => a.CustomerId == id)
                .FirstOrDefault();

            GetFoodContext.Customers.Remove(all);
            GetFoodContext.SaveChanges();

            return Json(GetFoodContext.Customers);
        }
    }
}
