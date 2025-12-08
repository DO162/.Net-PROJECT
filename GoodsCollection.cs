using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 6. GENERICS КОЛЕКЦІЯ
    public class GoodsCollection<T> : IEnumerable<T> where T : Goods // Колекція товарів через Generics (пошук по ID або назві, LINQ, TotalValue).
    {
        private List<T> _items = new List<T>(); // Внутрішній список товарів
        public T? this[int id] => _items.FirstOrDefault(g => g.Id == id); // Індексатор за ID
        public IEnumerable<T> this[string name] => _items.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase)); // Індексатор за назвою

        public void Add(T item) => _items.Add(item); // Метод для додавання товару
        public void AddRange(IEnumerable<T> items) => _items.AddRange(items); // Метод для додавання колекції товарів
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator(); // Реалізація IEnumerable
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _items.Count; // Властивість для отримання кількості товарів
        public decimal TotalValue => _items.Sum(g => g.Price * g.Quantity); //  Властивість для отримання загальної вартості товарів
        public IEnumerable<T> Where(Func<T, bool> predicate) => _items.Where(predicate); // Метод для фільтрації товарів
        public IEnumerable<T> OrderBy<TKey>(Func<T, TKey> keySelector) => _items.OrderBy(keySelector); // Метод для сортування товарів
    }
}
