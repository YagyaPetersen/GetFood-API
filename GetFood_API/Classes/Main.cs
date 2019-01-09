using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

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

        public List<object> Foods { get; set; }

        public bool DriverAcceptance { get; set; }

        public bool RestaurantAcceptance { get; set; }

        public long PickupTime { get; set; }

        public string OrderStatus { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal OverallFee { get; set; }

        public long DeliveryTime { get; set; }

        [Required]
        [MaxLength(70)]
        public string CustomerAddress { get; set; }
    }

    public class FoodOrder
    {
        [Key]
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        public Orders Orders { get; set; }

        public ICollection<int> FoodNumbers { get; set; }
        public Food Food{ get; set; }
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
        public long PrepTime { get; set; }

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
        public long PickupTime { get; set; }
        public long DeliveryTime { get; set; }
    }
}