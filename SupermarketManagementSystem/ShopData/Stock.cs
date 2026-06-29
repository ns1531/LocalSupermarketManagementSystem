using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketManagementSystem.ShopData
{
    public class Stock
    {
        public string ProductId { get; set; } = "";
        public int QuantityInStock { get; set; }
        public int LowStockThreshold { get; set; }
    }
}