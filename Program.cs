using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 14. ПРОГРАМА
    class Program // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    {
        static void Main() // ГОЛОВНИЙ МЕТОД ПРОГРАМИ
        {
            Console.Title = "🏪🛒 MarketPlace";
            Console.OutputEncoding = Encoding.UTF8;

            var shop = ShopManager.Instance;
            var customer = Customer.Instance;

            shop.LowStockAlert += (sender, msg) => // Обробник події низьких запасів
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[!] {msg}");
                Console.ResetColor();
            };

            customer.Cart.CartChanged += (sender, e) => // Обробник події зміни корзини
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n[Корзина] {e.Message}");
                Console.ResetColor();
            };

            MenuManager.MainMenu(shop, customer); // Викликаємо головне меню через MenuManager
        }
    }
}
