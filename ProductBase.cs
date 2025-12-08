using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 4. АБСТРАКТНИЙ КЛАС
    [Serializable] // Позначає клас як серіалізований
    public abstract class ProductBase // Абстрактний клас товару — базові поля товарів
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

        public abstract string GetDescription(); // Абстрактний метод для отримання опису продукту
    }
}
