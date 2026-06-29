using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketManagementSystem.ShopData
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public string SaleId { get; set; } = "";
        public string ProductId { get; set; } = "";
        public string ProductTitle { get; set; } = "";
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ItemTotal { get; set; }
    }
}