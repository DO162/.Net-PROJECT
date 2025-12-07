using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;




namespace MarketPlaceProject
{
    /// <summary>
    /// Клас товару
    /// </summary>
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public static Goods operator +(Goods g, int amount)
        {
            g.Quantity += amount;
            return g;
        }

        public static Goods operator -(Goods g, int amount)
        {
            g.Quantity -= amount;
            if (g.Quantity < 0) g.Quantity = 0;
            return g;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID:{Id}, Назва:{Name}, Ціна:{Price} грн, К-сть:{Quantity}, Категорія:{Category}");
        }
    }

    /// <summary>
    /// Інтерфейс оплати
    /// </summary>
    public interface IPay
    {
        void Pay();
    }

    /// <summary>
    /// Клас замовлення
    /// </summary>
    public class Basket : IPay
    {
        public Guid BasketId { get; set; }
        public Customer Customer { get; set; }
        public List<Goods> Items { get; set; } = new List<Goods>();
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public void CalcTotal()
        {
            TotalPrice = Items.Sum(x => x.Price * x.Quantity);
        }

        public void Pay()
        {
            Status = "Оплачено";
            Console.WriteLine($"Замовлення {BasketId} оплачено.");
        }
    }

    /// <summary>
    /// Клас клієнта
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Goods> Cart { get; set; } = new List<Goods>();

        public void AddToCart(Goods g, int qty)
        {
            if (g == null)
            {
                Console.WriteLine("Товар не знайдено!");
                return;
            }

            if (g.Quantity >= qty)
            {
                Cart.Add(new Goods { Id = g.Id, Name = g.Name, Price = g.Price, Quantity = qty, Category = g.Category });
                g.Quantity -= qty;
                Console.WriteLine($"{qty} шт. {g.Name} додано до корзини.");
            }
            else
            {
                Console.WriteLine($"Недостатньо товару {g.Name} на складі!");
            }
        }

        public Basket Checkout()
        {
            if (Cart.Count == 0)
            {
                Console.WriteLine("Корзина порожня!");
                return null;
            }
            var basket = new Basket { BasketId = Guid.NewGuid(), Customer = this, Items = new List<Goods>(Cart), Status = "Обробляється" };
            basket.CalcTotal();
            Cart.Clear();
            Console.WriteLine($"Замовлення {basket.BasketId} оформлено. Загальна сума: {basket.TotalPrice} грн.");
            return basket;
        }
    }

    /// <summary>
    /// Магазин (Singleton)
    /// </summary>
    public class Shop
    {
        private static Shop _instance;
        public static Shop Instance => _instance ??= new Shop();

        public List<Goods> GoodsList { get; set; } = new List<Goods>();
        public List<Basket> Baskets { get; set; } = new List<Basket>();

        public delegate void StockAlert(Goods g);
        public event StockAlert OnStockEmpty;

        public Goods this[int id]
        {
            get { return GoodsList.FirstOrDefault(x => x.Id == id); }
            set
            {
                var idx = GoodsList.FindIndex(x => x.Id == id);
                if (idx != -1) GoodsList[idx] = value;
                else GoodsList.Add(value);
            }
        }

        public void AddGoods(Goods g)
        {
            if (g != null)
            {
                GoodsList.Add(g);
                Console.WriteLine($"Товар {g.Name} додано до магазину.");
            }
        }

        public void RemoveGoods(Goods g)
        {
            if (g != null && GoodsList.Contains(g))
            {
                GoodsList.Remove(g);
                Console.WriteLine($"Товар {g.Name} видалено з магазину.");
            }
        }

        public Goods FindByName(string name)
        {
            return GoodsList.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void CheckStock()
        {
            foreach (var g in GoodsList)
            {
                if (g.Quantity <= 0)
                    OnStockEmpty?.Invoke(g);
            }
        }

        public void Save(string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(GoodsList, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var list = JsonSerializer.Deserialize<List<Goods>>(json);
                if (list != null) GoodsList = list;
            }
        }
    }

    /// <summary>
    /// Фабрика товарів
    /// </summary>
    public static class GoodsFactory
    {
        public static Goods Create(string category, int id, string name, decimal price, int qty)
        {
            if (string.IsNullOrEmpty(name)) name = "Без назви";
            switch (category?.ToLower())
            {
                case "electronics": return new Goods { Id = id, Name = name, Price = price, Quantity = qty, Category = "Електроніка" };
                case "clothing": return new Goods { Id = id, Name = name, Price = price, Quantity = qty, Category = "Одяг" };
                default: return new Goods { Id = id, Name = name, Price = price, Quantity = qty, Category = category ?? "Інше" };
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var shop = Shop.Instance;
            shop.OnStockEmpty += g => Console.WriteLine($"Увага! Товар {g.Name} закінчився на складі!");

            shop.AddGoods(GoodsFactory.Create("electronics", 1, "Ноутбук", 25000, 5));
            shop.AddGoods(GoodsFactory.Create("clothing", 2, "Футболка", 500, 10));

            var customer = new Customer { Id = 1, Name = "Іван", Email = "ivan@example.com" };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("\n=== Інтернет-магазин ===");
                Console.WriteLine("1. Переглянути товари");
                Console.WriteLine("2. Додати товар у корзину");
                Console.WriteLine("3. Переглянути корзину");
                Console.WriteLine("4. Оформити замовлення");
                Console.WriteLine("5. Вийти");
                Console.Write("Виберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foreach (var g in shop.GoodsList) g.ShowInfo();
                        break;
                    case "2":
                        try
                        {
                            Console.Write("ID товару: ");
                            if (!int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.WriteLine("Невірний ID!");
                                break;
                            }
                            Console.Write("Кількість: ");
                            if (!int.TryParse(Console.ReadLine(), out int qty))
                            {
                                Console.WriteLine("Невірна кількість!");
                                break;
                            }
                            var item = shop[id];
                            if (item != null) customer.AddToCart(item, qty);
                            else Console.WriteLine("Товар не знайдено!");
                        }
                        catch { Console.WriteLine("Помилка вводу!"); }
                        break;
                    case "3":
                        if (customer.Cart.Count == 0) Console.WriteLine("Корзина порожня!");
                        else customer.Cart.ForEach(g => g.ShowInfo());
                        break;
                    case "4":
                        var basket = customer.Checkout();
                        if (basket != null) shop.Baskets.Add(basket);
                        break;
                    case "5":
                        shop.Save("goods.json");
                        Console.WriteLine("Дані збережено. До побачення!");
                        return;
                    default: Console.WriteLine("Невірна команда!"); break;
                }

                shop.CheckStock();
            }
        }
    }
}
