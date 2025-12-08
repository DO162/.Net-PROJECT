using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 10. SHOP MANAGER - робота з товарами
    public sealed class ShopManager // Singleton магазину – зберігає доступні товари, перевіряє залишки, фільтрує.
    {
        // Приватне статичне поле для зберігання єдиного екземпляра
        private static ShopManager? _instance;
        public static ShopManager Instance => _instance ??= new ShopManager();
        public GoodsCollection<Goods> Goods { get; } = new GoodsCollection<Goods>();
        public event StockEventHandler? LowStockAlert;

        private ShopManager() // Приватний конструктор
        {
            // Ініціалізація товарів
            Goods.AddRange(GoodsData.GetAllGoods());
            CheckStock();
        }

        public void CheckStock() // Метод для перевірки запасів
        {
            foreach (var g in Goods.Where(g => g.Quantity <= 3)) // Перевіряємо товари з кількістю менше або рівною 3
                LowStockAlert?.Invoke(this, $"Увага! Закінчується: {g.Name} (залишилось: {g.Quantity})"); // Викликаємо подію для кожного товару з низьким запасом
        }

        public IEnumerable<Goods> Search(string query) => Goods[query]; // Метод для пошуку товарів за назвою
        public IEnumerable<Goods> FilterByPrice(decimal min, decimal max) =>
            Goods.Where(g => g.Price >= min && g.Price <= max).OrderBy(g => g.Price); // Метод для фільтрації товарів за ціною
    }
}
