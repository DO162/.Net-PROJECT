using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 12. ІНТЕРФЕЙС УТИЛІТИ
    public static class Interface // СТАТИЧНИЙ КЛАС ІНТЕРФЕЙСУ
    {
        public static void ShowHeader(string title) // Метод для відображення заголовка
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n=== {title} ===");
            Console.ResetColor();
        }

        public static int GetChoice(int min, int max) // Метод для отримання вибору користувача
        {
            while (true)
            {
                Console.Write("\nВаш вибір: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max) // Перевірка на коректність вводу
                    return choice;
                Console.WriteLine($"Потрібно число від {min} до {max}");
            }
        }

        public static void ShowProduct(Goods g) // Метод для відображення інформації про товар
        {
            Console.ForegroundColor = g.Quantity <= 3 ? ConsoleColor.Yellow : ConsoleColor.Green; // Колір залежно від кількості
            Console.WriteLine($"[{g.Id}] {g.Name,-25} | {g.Category,-15} | {g.Price,8} грн | {g.Quantity,3} шт.");
            Console.ResetColor(); // Скидання кольору
        }
    }
}
