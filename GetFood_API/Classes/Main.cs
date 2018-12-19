using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetFood_API.Classes
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
    }

    public class Driver
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
    }

    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RestaurantName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

    }
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Driver")]
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public ICollection<Food> Foods { get; set; }

        public bool DriverAcceptance { get; set; }

        public bool RestaurantAcceptance { get; set; }

        public DateTime PickupTime { get; set; }

        public string OrderStatus { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal OverallFee { get; set; }

        public DateTime DeliveryTime { get; set; }

        [Required]
        [MaxLength(70)]
        public string CustomerAddress { get; set; }
    }

    public class FoodOrder
    {
        [Key]
        public int FoodOrderId { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        public Orders Orders { get; set; }

        [ForeignKey("Food")]
        public int[] Foods { get; set; }
        public Food Food { get; set; }

    }

    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        [MaxLength(25)]
        public string FoodName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime PrepTime { get; set; }

        [ForeignKey("Restaurant")]
        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }

    public class OrderRequest
    {
        public int RequestId { get; set; }
        public bool RestaurantAcceptance { get; set; }
        public bool DriverAcceptance { get; set; }
        public int DriverId { get; set; }
        public int DeliveryFee { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}