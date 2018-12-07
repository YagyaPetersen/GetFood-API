using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

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

        [Required]
        public bool DriverAcceptance { get; set; }

        [Required]
        public bool RestaurantAcceptance { get; set; }

        [Required]
        public DateTime PickupTime { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public decimal DeliveryFee { get; set; }

        [Required]
        public decimal OverallFee { get; set; }
 
        [Required]
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
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }

    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        //[ForeignKey("Orders")]
        //public int OrderId { get; set; }
        
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
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

       // public virtual Orders Orders { get; set; }
    }

    public class OrderRequest
    {
        public int RequestId { get; set; }
        public bool RestaurantAcceptance { get; set; }
        public bool DriverAcceptance { get; set; }
        public int DriverId { get; set; }
        public FoodOrder details { get; set; }
        public int DeliveryFee { get; set; }
    }
}