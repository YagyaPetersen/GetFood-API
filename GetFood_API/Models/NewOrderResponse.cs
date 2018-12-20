using GetFood_API.Classes;

namespace GetFood_API.Models
{
    public class NewOrderResponse
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string CustomerAddress { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public bool RestaurantAcceptance { get; set; }
        public bool DriverAcceptance { get; set; }
        public string OrderStatus { get; set; }

        public NewOrderResponse(Orders orderInfo)
        {
            OrderId = orderInfo.OrderId;
            FullName = orderInfo.Customer.FirstName + " " + orderInfo.Customer.LastName;
            CustomerAddress = orderInfo.CustomerAddress;
            RestaurantName = orderInfo.Restaurant.RestaurantName;
            RestaurantAddress = orderInfo.Restaurant.Address;
            RestaurantAcceptance = orderInfo.RestaurantAcceptance;
            DriverAcceptance = orderInfo.DriverAcceptance;
            OrderStatus = orderInfo.OrderStatus;
        }
    }
}
