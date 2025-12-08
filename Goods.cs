using MarketPlaceProject.Interfaces;
using System;

namespace MarketPlaceProject.Models
{
    // КЛАС ТОВАРУ
    [Serializable]
    public class Goods : ProductBase, ICloneable, ISerializableEntity
    {
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = value >= 0 ? value : throw new ArgumentException("Кількість не може бути від'ємною");
        }

        public Goods(int id, string name, decimal price, int quantity, string category)
            : base(id, name, price, category) => Quantity = quantity;

        public override string ToString() => $"{Name} - {Price} грн ({Quantity} шт.)";
        public override string GetDescription() => $"Товар: {Name}, Категорія: {Category}";
        public object Clone() => new Goods(Id, Name, Price, Quantity, Category);
        public string Serialize() => $"{Id}|{Name}|{Price}|{Quantity}|{Category}";

        public static Goods operator +(Goods g, int qty)
        {
            var result = (Goods)g.Clone();
            result.Quantity += qty;
            return result;
        }

        public static bool operator ==(Goods a, Goods b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Goods a, Goods b) => !(a == b);
        public override bool Equals(object? obj) => obj is Goods goods && this == goods;
        public override int GetHashCode() => Id.GetHashCode();
    }
}