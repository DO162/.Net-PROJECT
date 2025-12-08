using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketPlaceProject.Models
{
    // ЗАМОВЛЕННЯ
    [Serializable]
    public class Order
    {
        public int Id { get; }
        public DateTime Date { get; }
        public List<Goods> Items { get; }
        public decimal Total { get; }

        public Order(int id, List<Goods> items)
        {
            Id = id;
            Date = DateTime.Now;
            Items = items.Select(i => (Goods)i.Clone()).ToList();
            Total = items.Sum(i => i.Price * i.Quantity);
        }

        public override string ToString() =>
            $"Замовлення #{Id} від {Date:dd.MM.yyyy} - {Total} грн";
    }
}