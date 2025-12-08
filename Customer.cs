using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 8. КЛАС ПОКУПЦЯ
    public sealed class Customer // Singleton покупця — його ім’я, телефон, адреса, email, корзина, історія замовлень.
    {
        private static Customer? _instance; // Приватне статичне поле для зберігання єдиного екземпляра
        public static Customer Instance => _instance ??= new Customer(); // Властивість для отримання єдиного екземпляра

        // Властивості покупця
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public ShoppingCart Cart { get; } = new ShoppingCart();
        public List<Order> Orders { get; } = new List<Order>();

        private Customer() // Приватний конструктор
        {
            // Значення за замовчуванням
            Name = "Гість";
            Phone = "+380-21184823313";
            Address = "Місто, вулиця, будинок";
            Email = "guest@example.com";
        }
    }
}
