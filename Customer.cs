using System.Collections.Generic;
using MarketPlaceProject.Models;

namespace MarketPlaceProject.Services
{
    // SINGLETON покупець
    public sealed class Customer
    {
        private static Customer? _instance;
        public static Customer Instance => _instance ??= new Customer();

        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public ShoppingCart Cart { get; } = new ShoppingCart();
        public List<Order> Orders { get; } = new List<Order>();

        private Customer()
        {
            Name = "Гість";
            Phone = "+380-21184823313";
            Address = "Місто, вулиця, будинок";
            Email = "guest@example.com";
        }
    }
}