using System;
using MarketPlaceProject.Models;

namespace MarketPlaceProject.Utils
{
    // UI УТИЛІТИ
    public static class UI
    {
        public static void ShowHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n=== {title} ===");
            Console.ResetColor();
        }

        public static int GetChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("\nВаш вибір: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                    return choice;
                Console.WriteLine($"Потрібно число від {min} до {max}");
            }
        }

        public static void ShowProduct(Goods g)
        {
            Console.ForegroundColor = g.Quantity <= 3 ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.WriteLine($"[{g.Id}] {g.Name,-25} | {g.Category,-15} | {g.Price,8} грн | {g.Quantity,3} шт.");
            Console.ResetColor();
        }
    }
}