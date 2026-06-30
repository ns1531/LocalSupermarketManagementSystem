using System;

using SupermarketManagementSystem.ShopData;
using SupermarketManagementSystem.ShopDataStructures;

namespace SupermarketManagementSystem.ShopDatabase
{
    public class ShopDataLoader
    {
        public static ProductCatalogueArray LoadProducts()
        {
            ProductCatalogueArray productCatalogue = new ProductCatalogueArray();

            using ShopDbContext database = new ShopDbContext();

            Product[] products = database.Products.ToArray();

            for (int i = 0; i < products.Length; i++)
            {
                Product product = products[i];

                Stock? stock = database.Stock.Find(product.ProductId);

                if (stock != null)
                {
                    product.QuantityInStock = stock.QuantityInStock;
                    product.LowStockThreshold = stock.LowStockThreshold;
                }

                productCatalogue.AddProduct(product);
            }

            return productCatalogue;
        }

        public static SupplierRecordArray LoadSuppliers()
        {
            SupplierRecordArray supplierRecords = new SupplierRecordArray();

            using ShopDbContext database = new ShopDbContext();

            foreach (Supplier supplier in database.Suppliers)
            {
                supplierRecords.AddSupplier(supplier);
            }

            return supplierRecords;
        }
    }
}