using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 7. КОРЗИНА
    public class ShoppingCart // Корзина - додає товари, рахує суму, зберігає список товарів, викликає події.
    {
        private List<Goods> _items = new List<Goods>();
        public event CartEventHandler? CartChanged;
        public IReadOnlyList<Goods> Items => _items.AsReadOnly(); // Властивість для отримання списку товарів у корзині
        public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity); // Властивість для отримання загальної вартості корзини

        public void AddItem(Goods goods, int quantity) // Метод для додавання товару у корзину
        {
            if (goods.Quantity < quantity)
                throw new InvalidOperationException($"Недостатньо товару. Доступно: {goods.Quantity}");

            var existing = _items.FirstOrDefault(i => i.Id == goods.Id); // Перевіряємо чи товар вже є у корзині
            if (existing is not null)
                existing.Quantity += quantity;
            else _items.Add((Goods)goods.Clone());

            goods.Quantity -= quantity;
            OnCartChanged($"Додано: {goods.Name} x{quantity}");
        }

        public void Clear() // Метод для очищення корзини
        {
            _items.Clear();
            OnCartChanged("Корзина очищена");
        }

        protected virtual void OnCartChanged(string message) =>
            CartChanged?.Invoke(this, new CartEventArgs(message)); // Викликаємо подію зміни корзини
    }
}
