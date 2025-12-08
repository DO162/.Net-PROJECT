using System;
using System.Collections.Generic;
using System.Linq;
using MarketPlaceProject.Models;

namespace MarketPlaceProject.Collections
{
    // GENERICS КОЛЕКЦІЯ
    public class GoodsCollection<T> : IEnumerable<T> where T : Goods
    {
        private List<T> _items = new List<T>();
        public T? this[int id] => _items.FirstOrDefault(g => g.Id == id);
        public IEnumerable<T> this[string name] => _items.Where(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        public void Add(T item) => _items.Add(item);
        public void AddRange(IEnumerable<T> items) => _items.AddRange(items);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _items.Count;
        public decimal TotalValue => _items.Sum(g => g.Price * g.Quantity);
        public IEnumerable<T> Where(Func<T, bool> predicate) => _items.Where(predicate);
        public IEnumerable<T> OrderBy<TKey>(Func<T, TKey> keySelector) => _items.OrderBy(keySelector);
    }
}