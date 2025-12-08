using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //---------------------------------------------------------------
    // 1. СТАТИЧНИЙ КЛАС ДАНИХ ТОВАРІВ
    public static class GoodsData // Зберігає всі товари і список категорій. Дасть дані магазину при старті.
    {
        private static int _nextId = 1;

        public static List<Goods> GetAllGoods()
        {
            return new List<Goods>
            {
                // Смартфони, ТВ та електроніка
                new Goods(_nextId++, "iPhone 15 Pro", 45999, 10, "Смартфони, ТВ та електроніка"),
                new Goods(_nextId++, "Samsung Galaxy S23", 34999, 15, "Смартфони, ТВ та електроніка"),
                new Goods(_nextId++, "Xiaomi 13 Pro", 28999, 8, "Смартфони, ТВ та електроніка"),
                new Goods(_nextId++, "Sony Bravia 55\"", 25999, 5, "Смартфони, ТВ та електроніка"),
                new Goods(_nextId++, "LG OLED 65\"", 59999, 3, "Смартфони, ТВ та електроніка"),

                // Ноутбуки та комп'ютери
                new Goods(_nextId++, "MacBook Pro M3", 74999, 4, "Ноутбуки та комп'ютери"),
                new Goods(_nextId++, "Dell XPS 15", 69999, 6, "Ноутбуки та комп'ютери"),
                new Goods(_nextId++, "Lenovo ThinkPad", 45999, 8, "Ноутбуки та комп'ютери"),
                new Goods(_nextId++, "HP Spectre x360", 52999, 5, "Ноутбуки та комп'ютери"),
                new Goods(_nextId++, "Asus ROG Strix", 64999, 3, "Ноутбуки та комп'ютери"),

                // Товари для геймерів
                new Goods(_nextId++, "PlayStation 5", 20999, 3, "Товари для геймерів"),
                new Goods(_nextId++, "Xbox Series X", 19999, 4, "Товари для геймерів"),
                new Goods(_nextId++, "Nintendo Switch", 13999, 6, "Товари для геймерів"),
                new Goods(_nextId++, "Razer Gaming Mouse", 2999, 10, "Товари для геймерів"),
                new Goods(_nextId++, "Logitech Gaming Keyboard", 3999, 8, "Товари для геймерів"),

                // Побутова техніка
                new Goods(_nextId++, "LG Холодильник", 48999, 7, "Побутова техніка"),
                new Goods(_nextId++, "Dyson Пилосос", 25999, 9, "Побутова техніка"),
                new Goods(_nextId++, "Bosch Пральна машина", 32999, 6, "Побутова техніка"),
                new Goods(_nextId++, "Philips Мікрохвильова", 6999, 12, "Побутова техніка"),
                new Goods(_nextId++, "Redmond Кавоварка", 4999, 15, "Побутова техніка")
            };
        }

        public static List<string> GetCategories() // Метод для отримання списку категорій товарів
        {
            return new List<string>
            {
                "Смартфони, ТВ та електроніка",
                "Ноутбуки та комп'ютери",
                "Товари для геймерів",
                "Побутова техніка"
            };
        }
    }
}
