 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 9. ЗАМОВЛЕННЯ
    [Serializable] // Позначає клас як серіалізований
    public class Order // Одне замовлення — список товарів, дата, загальна сума.
    {
        // Властивості замовлення
        public int Id { get; }
        public DateTime Date { get; }
        public List<Goods> Items { get; }
        public decimal Total { get; }

        public Order(int id, List<Goods> items) // Конструктор
        {
            Id = id;
            Date = DateTime.Now;
            Items = items.Select(i => (Goods)i.Clone()).ToList();
            Total = items.Sum(i => i.Price * i.Quantity);
        }

        public override string ToString() =>
            $"Замовлення #{Id} від {Date:dd.MM.yyyy} - {Total} грн"; // Переопределение метода ToString
    }
}
