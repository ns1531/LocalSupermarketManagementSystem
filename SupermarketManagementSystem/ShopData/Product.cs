using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketManagementSystem.ShopData
{
    public class Product
    {
        public string ProductId { get; set; } = "";
        public string Barcode { get; set; } = "";
        public string Title { get; set; } = "";
        public string Brand { get; set; } = "";
        public string CategoryId { get; set; } = "";
        public string SupplierId { get; set; } = "";
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int LowStockThreshold { get; set; }
        public DateTime RestockDate { get; set; }

        public string StockStatus
        {
            get
            {
                if (QuantityInStock <= 0)
                {
                    return "Out of Stock";
                }

                if (QuantityInStock <= LowStockThreshold)
                {
                    return "Low Stock";
                }

                return "In Stock";
            }
        }
    }
}