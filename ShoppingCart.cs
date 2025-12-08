using System;
using System.Collections.Generic;
using System.Linq;
using MarketPlaceProject.Models;
using MarketPlaceProject.Events;

namespace MarketPlaceProject.Services
{
    // КОРЗИНА
    public class ShoppingCart
    {
        private List<Goods> _items = new List<Goods>();
        public event CartEventHandler? CartChanged;
        public IReadOnlyList<Goods> Items => _items.AsReadOnly();
        public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

        public void AddItem(Goods goods, int quantity)
        {
            if (goods.Quantity < quantity)
                throw new InvalidOperationException($"Недостатньо товару. Доступно: {goods.Quantity}");

            var existing = _items.FirstOrDefault(i => i.Id == goods.Id);
            if (existing is not null)
                existing.Quantity += quantity;
            else
                _items.Add((Goods)goods.Clone());

            goods.Quantity -= quantity;
            OnCartChanged($"Додано: {goods.Name} x{quantity}");
        }

        public void Clear()
        {
            _items.Clear();
            OnCartChanged("Корзина очищена");
        }

        protected virtual void OnCartChanged(string message) =>
            CartChanged?.Invoke(this, new CartEventArgs(message));
    }
}