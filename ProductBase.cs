using System;

namespace MarketPlaceProject.Models
{
    // АБСТРАКТНИЙ КЛАС
    [Serializable]
    public abstract class ProductBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public string Category { get; protected set; }

        protected ProductBase(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public abstract string GetDescription();
    }
}