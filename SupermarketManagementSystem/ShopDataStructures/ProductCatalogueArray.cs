using System;
using System.Collections.Generic;
using System.Text;

using SupermarketManagementSystem.ShopData;

namespace SupermarketManagementSystem.ShopDataStructures
{
    public class ProductCatalogueArray
    {
        private Product[] products;
        private int count;

        public ProductCatalogueArray()
        {
            products = new Product[5];
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void AddProduct(Product product)
        {
            if (count == products.Length)
            {
                ResizeArray();
            }

            products[count] = product;
            count++;
        }

        public Product? GetProductAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return null;
            }

            return products[index];
        }

        public Product? SearchByName(string productName)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Title.ToLower().Contains(productName.ToLower()))
                {
                    return products[i];
                }
            }

            return null;
        }

        public Product? SearchByProductId(string productId)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].ProductId == productId)
                {
                    return products[i];
                }
            }

            return null;
        }

        public bool BarcodeExists(string barcode)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Barcode == barcode)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RemoveProduct(string productId)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].ProductId == productId)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        products[j] = products[j + 1];
                    }

                    count--;
                    return true;
                }
            }

            return false;
        }

        private void ResizeArray()
        {
            Product[] largerArray = new Product[products.Length * 2];

            for (int i = 0; i < count; i++)
            {
                largerArray[i] = products[i];
            }

            products = largerArray;
        }
    }
}