using System;
using System.Collections.Generic;
using System.Text;

using SupermarketManagementSystem.ShopData;

namespace SupermarketManagementSystem.ShopDataStructures
{
    public class BarcodeIndexTable
    {
        private Product?[] products;

        public BarcodeIndexTable()
        {
            products = new Product?[20];
        }

        public void Clear()
        {
            products = new Product?[20];
        }

        public void AddProduct(Product product)
        {
            int index = HashBarcode(product.Barcode);

            for (int i = 0; i < products.Length; i++)
            {
                if (products[index] == null)
                {
                    products[index] = product;
                    return;
                }

                index = (index + 1) % products.Length;
            }
        }

        public Product? SearchByBarcode(string barcode)
        {
            int index = HashBarcode(barcode);

            for (int i = 0; i < products.Length; i++)
            {
                if (products[index] == null)
                {
                    return null;
                }

                if (products[index]!.Barcode == barcode)
                {
                    return products[index];
                }

                index = (index + 1) % products.Length;
            }

            return null;
        }

        private int HashBarcode(string barcode)
        {
            int total = 0;

            for (int i = 0; i < barcode.Length; i++)
            {
                total += barcode[i];
            }

            return total % products.Length;
        }
    }
}