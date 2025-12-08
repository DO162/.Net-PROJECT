using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 11. FILE MANAGER - робота з файлами
    public static class FileManager // Статичний клас для роботи з файлами (збереження замовлень у файл).
    {
        private const string FilePath = "marketplace_data.txt";
        private static bool _isFirstRun = true; // Очищення файлу при першому запуску

        // Метод для збереження останнього замовлення у файл
        public static void SaveLastOrder(Customer customer)
        {
            if (!customer.Orders.Any()) return;

            var lastOrder = customer.Orders.Last();
            var sb = new StringBuilder();

            // Очищення файлу при першому запуску програми
            if (_isFirstRun)
            {
                File.WriteAllText(FilePath, string.Empty, Encoding.UTF8);
                _isFirstRun = false;
            }

            // --- OrderedGoods ---
            sb.AppendLine("#OrderedGoods");
            foreach (var g in lastOrder.Items.GroupBy(i => i.Name)
                                             .Select(g => new { Name = g.Key, Quantity = g.Sum(x => x.Quantity) }))
            {
                sb.AppendLine($"{g.Name}|{g.Quantity}");
            }

            // --- Orders ---
            sb.AppendLine("#Orders");
            foreach (var item in lastOrder.Items)
            {
                sb.AppendLine($"{lastOrder.Id}|{lastOrder.Date:yyyy-MM-dd HH:mm}|{customer.Name}|{item.Name}|{item.Quantity}|{item.Price * item.Quantity}");
            }

            sb.AppendLine("------------------------------------------------------");

            // Додаємо блок у кінець файлу
            File.AppendAllText(FilePath, sb.ToString(), Encoding.UTF8);
        }
    }
}
