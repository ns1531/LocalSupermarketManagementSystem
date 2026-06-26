using System;
using System.Collections.Generic;
using System.Text;

using SupermarketManagementSystem.ShopData;

namespace SupermarketManagementSystem.ShopDataStructures
{
    public class SaleRecordArray
    {
        private Sale[] sales;
        private int count;

        public SaleRecordArray()
        {
            sales = new Sale[5];
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void AddSale(Sale sale)
        {
            if (count == sales.Length)
            {
                ResizeArray();
            }

            sales[count] = sale;
            count++;
        }

        public Sale? GetSaleAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return null;
            }

            return sales[index];
        }

        private void ResizeArray()
        {
            Sale[] largerArray = new Sale[sales.Length * 2];

            for (int i = 0; i < count; i++)
            {
                largerArray[i] = sales[i];
            }

            sales = largerArray;
        }
    }
}