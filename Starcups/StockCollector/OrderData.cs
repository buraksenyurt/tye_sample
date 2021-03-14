using System;

namespace StockCollector
{
    public class OrderData
    {
        public string ShopName { get; set; }

        public string ItemName { get; set; }

        public double Quantity { get; set; }
        public DateTime Time { get; set; }
    }
}
