using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceProject
{
    //-------------------------------------------------------------------
    // 3. ДЕЛЕГАТИ та ПОДІЇ
    public delegate void StockEventHandler(object sender, string message); // Делегат для подій запасів (попередження про закінчення товарів)
    public delegate void CartEventHandler(object sender, CartEventArgs e); // Делегат для подій корзини (зміни корзини)

    public class CartEventArgs : EventArgs // Клас для аргументів подій корзини
    {
        public string Message { get; } // Повідомлення події
        public CartEventArgs(string message) => Message = message; // Конструктор
    }
}
