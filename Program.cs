using System;
using System.Linq;
using System.Text;
using MarketPlaceProject.Services;
using MarketPlaceProject.Models;
using MarketPlaceProject.Data;
using MarketPlaceProject.Utils;

namespace MarketPlaceProject
{
    // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    class Program
    {
        static void Main()
        {
            Console.Title = "MarketPlace";
            Console.OutputEncoding = Encoding.UTF8;

            var shop = ShopManager.Instance;
            var customer = Customer.Instance;

            shop.LowStockAlert += (sender, msg) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[!] {msg}");
                Console.ResetColor();
            };

            customer.Cart.CartChanged += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n[Корзина] {e.Message}");
                Console.ResetColor();
            };

            MainMenu(shop, customer);
        }

        static void MainMenu(ShopManager shop, Customer customer)
        {
            while (true)
            {
                UI.ShowHeader("Головне меню");
                Console.WriteLine($"Користувач: {customer.Name}");
                Console.WriteLine($"Корзина: {customer.Cart.Items.Count} товарів на {customer.Cart.TotalPrice} грн\n");

                Console.WriteLine("1.Каталог товарів");
                Console.WriteLine("2.Корзина");
                Console.WriteLine("3.Замовлення");
                Console.WriteLine("4.Налаштування");
                Console.WriteLine("5.Файли");
                Console.WriteLine("6.Вийти");

                switch (UI.GetChoice(1, 6))
                {
                    case 1: ShowCatalog(shop, customer); break;
                    case 2: ShowCart(customer); break;
                    case 3: ShowOrders(customer); break;
                    case 4: Settings(customer); break;
                    case 5: FileOperations(customer); break;
                    case 6:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n======================================");
                        Console.WriteLine("Дякуємо за покупки, до зустрічі!");
                        Console.WriteLine("======================================\n");
                        Console.ResetColor();
                        Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
                        Console.ReadKey();
                        return;
                }
            }
        }

        static void ShowCatalog(ShopManager shop, Customer customer)
        {
            while (true)
            {
                UI.ShowHeader("Каталог товарів");

                var categories = GoodsData.GetCategories();

                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {categories[i]}");
                }
                Console.WriteLine("0. Назад");

                int choice = UI.GetChoice(0, categories.Count);
                if (choice == 0) break;

                string selectedCategory = categories[choice - 1];
                var productsInCategory = shop.Goods.Where(g => g.Category == selectedCategory).ToList();

                while (true)
                {
                    UI.ShowHeader($"Категорія: {selectedCategory}");
                    foreach (var g in productsInCategory)
                        UI.ShowProduct(g);

                    Console.WriteLine("\n0. Назад");
                    Console.WriteLine("Введіть ID товару, щоб додати у корзину");

                    int id = UI.GetChoice(0, shop.Goods.Max(g => g.Id));
                    if (id == 0) break;

                    var product = shop.Goods[id];
                    if (product is not null && product.Category == selectedCategory)
                        AddToCart(product, customer);
                    else
                    {
                        Console.WriteLine("Товар не знайдено у цій категорії");
                        Console.ReadKey();
                    }
                }
            }
        }

        static void AddToCart(Goods product, Customer customer)
        {
            Console.Write($"Кількість (до {product.Quantity}): ");
            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
            {
                try
                {
                    customer.Cart.AddItem(product, qty);
                    Console.WriteLine("Додано до корзини");
                }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
            }
        }

        static void ShowCart(Customer customer)
        {
            while (true)
            {
                UI.ShowHeader("Ваша корзина");

                if (!customer.Cart.Items.Any())
                    Console.WriteLine("Корзина порожня");
                else
                {
                    foreach (var item in customer.Cart.Items)
                        Console.WriteLine($"{item.Name} x{item.Quantity} = {item.Price * item.Quantity} грн");

                    Console.WriteLine($"\nЗагальна сума: {customer.Cart.TotalPrice} грн");
                }

                Console.WriteLine("\n1. Оформити замовлення");
                Console.WriteLine("2. Очистити корзину");
                Console.WriteLine("0. Назад");

                switch (UI.GetChoice(0, 2))
                {
                    case 1: Checkout(customer); return;
                    case 2: customer.Cart.Clear(); Console.WriteLine("✅ Корзина очищена"); break;
                    case 0: return;
                }
            }
        }

        static void Checkout(Customer customer)
        {
            if (!customer.Cart.Items.Any())
            {
                Console.WriteLine("Корзина порожня!");
                return;
            }

            Console.WriteLine("\nВикористати існуючі дані профілю або ввести нові?");
            Console.WriteLine("1. Використати існуючі");
            Console.WriteLine("2. Ввести нові");

            int choice = UI.GetChoice(1, 2);
            if (choice == 2)
            {
                Console.Write("Ваше ім'я: ");
                customer.Name = Console.ReadLine();

                Console.Write("Телефон: ");
                customer.Phone = Console.ReadLine();

                Console.Write("Адреса: ");
                customer.Address = Console.ReadLine();

                Console.Write("Аккаунт: ");
                customer.Email = Console.ReadLine();
            }

            var order = new Order(customer.Orders.Count + 1, customer.Cart.Items.ToList());
            customer.Orders.Add(order);
            customer.Cart.Clear();

            Console.WriteLine($"\nЗамовлення #{order.Id} оформлено для {customer.Name}!");
            Console.WriteLine($"Сума: {order.Total} грн");

            Console.Write("Бажаєте зберегти замовлення у файл? (T - так/F - ні): ");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.T)
            {
                FileManager.SaveLastOrder(customer);
                Console.WriteLine("Замовлення збережене у файл marketplace_data.txt");
            }
            else
            {
                Console.WriteLine("Замовлення не збережене у файл");
            }

            Console.ReadKey();
        }

        static void ShowOrders(Customer customer)
        {
            UI.ShowHeader("Мої замовлення");

            if (!customer.Orders.Any())
                Console.WriteLine("У вас ще немає замовлень");
            else
                foreach (var order in customer.Orders)
                    Console.WriteLine(order);

            Console.ReadKey();
        }

        static void Settings(Customer customer)
        {
            while (true)
            {
                UI.ShowHeader("Налаштування профілю");
                Console.WriteLine($"1. Ім'я: {customer.Name}");
                Console.WriteLine($"2. Телефон: {customer.Phone}");
                Console.WriteLine($"3. Адреса: {customer.Address}");
                Console.WriteLine($"4. Аккаунт: {customer.Email}");
                Console.WriteLine("0. Назад");

                int choice = UI.GetChoice(0, 4);
                if (choice == 0) break;

                Console.Write("Введіть нове значення: ");
                switch (choice)
                {
                    case 1: customer.Name = Console.ReadLine(); break;
                    case 2: customer.Phone = Console.ReadLine(); break;
                    case 3: customer.Address = Console.ReadLine(); break;
                    case 4: customer.Email = Console.ReadLine(); break;
                }

                Console.WriteLine("Значення оновлено");
                Console.ReadKey();
            }
        }

        static void FileOperations(Customer customer)
        {
            UI.ShowHeader("Робота з файлами");

            FileManager.SaveLastOrder(customer);

            Console.WriteLine("Замовлення збережене у файл marketplace_data.txt");

            Console.ReadKey();
        }
    }
}