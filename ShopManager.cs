using MarketPlaceProject.Collections;
using MarketPlaceProject.Data;
using MarketPlaceProject.Events;
using MarketPlaceProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace MarketPlaceProject.Services
{
    // SHOP MANAGER
    public sealed class ShopManager
    {
        private static ShopManager? _instance;
        public static ShopManager Instance => _instance ??= new ShopManager();
        public GoodsCollection<Goods> Goods { get; } = new GoodsCollection<Goods>();
        public event StockEventHandler? LowStockAlert;

        private ShopManager()
        {
            Goods.AddRange(GoodsData.GetAllGoods());
            CheckStock();
        }

        public void CheckStock()
        {
            foreach (var g in Goods.Where(g => g.Quantity <= 3))
                LowStockAlert?.Invoke(this, $"Увага! Закінчується: {g.Name} (залишилось: {g.Quantity})");
        }

        public IEnumerable<Goods> Search(string query) => Goods[query];
        public IEnumerable<Goods> FilterByPrice(decimal min, decimal max) =>
            Goods.Where(g => g.Price >= min && g.Price <= max).OrderBy(g => g.Price);
    }
}