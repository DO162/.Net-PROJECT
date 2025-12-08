using System;

namespace MarketPlaceProject.Events
{
    // ДЕЛЕГАТИ та ПОДІЇ
    public delegate void StockEventHandler(object sender, string message);
    public delegate void CartEventHandler(object sender, CartEventArgs e);

    public class CartEventArgs : EventArgs
    {
        public string Message { get; }
        public CartEventArgs(string message) => Message = message;
    }
}