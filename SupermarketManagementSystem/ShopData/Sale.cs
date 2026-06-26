using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketManagementSystem.ShopData
{
    public class Sale
    {
        public string SaleId { get; set; } = "";
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public SaleItem Item { get; set; } = new SaleItem();
    }
}