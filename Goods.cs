using MarketPlaceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 5. КЛАС ТОВАРУ
    [Serializable] // Позначає клас як серіалізований
    public class Goods : ProductBase, ICloneable, ISerializableEntity // Конкретний товар (кількість, клонування, серіалізація, ToString, оператори +, ==).
    {
        private int _quantity;
        public int Quantity
        {
            get => _quantity; // Геттер
            set => _quantity = value >= 0 ? value : throw new ArgumentException("Кількість не може бути від'ємною"); // Сеттер з перевіркою
        }

        public Goods(int id, string name, decimal price, int quantity, string category)
            : base(id, name, price, category) => Quantity = quantity; // Конструктор

        public override string ToString() => $"{Name} - {Price} грн ({Quantity} шт.)"; // Переопределение метода ToString
        public override string GetDescription() => $"Товар: {Name}, Категорія: {Category}"; // Переопределение абстрактного метода
        public object Clone() => new Goods(Id, Name, Price, Quantity, Category); // Реалізація методу клонування
        public string Serialize() => $"{Id}|{Name}|{Price}|{Quantity}|{Category}"; // Реалізація методу серіалізації 

        public static Goods operator +(Goods g, int qty) // Перевантаження оператора +
        {
            var result = (Goods)g.Clone();
            result.Quantity += qty;
            return result;
        }

        public static bool operator ==(Goods a, Goods b) // Перевантаження оператора ==
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Goods a, Goods b) => !(a == b); // Перевантаження оператора !=
        public override bool Equals(object? obj) => obj is Goods goods && this == goods;
        public override int GetHashCode() => Id.GetHashCode();
    }
}
