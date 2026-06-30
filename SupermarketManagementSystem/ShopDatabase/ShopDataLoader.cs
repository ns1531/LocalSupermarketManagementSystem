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

        public static SaleRecordArray LoadSales()
        {
            SaleRecordArray saleRecords = new SaleRecordArray();

            using ShopDbContext database = new ShopDbContext();

            Sale[] sales = database.Sales.ToArray();
            SaleItem[] saleItems = database.SaleItems.ToArray();

            for (int i = 0; i < sales.Length; i++)
            {
                Sale sale = sales[i];

                for (int j = 0; j < saleItems.Length; j++)
                {
                    if (saleItems[j].SaleId == sale.SaleId)
                    {
                        sale.Item = saleItems[j];
                        break;
                    }
                }

                saleRecords.AddSale(sale);
            }

            return saleRecords;
        }

        public static void UpdateStockQuantity(string productId, int newQuantity)
        {
            using ShopDbContext database = new ShopDbContext();

            Stock? stock = database.Stock.Find(productId);

            if (stock != null)
            {
                stock.QuantityInStock = newQuantity;
                database.SaveChanges();
            }
        }

        public static void SaveSale(Sale sale)
        {
            using ShopDbContext database = new ShopDbContext();

            database.Sales.Add(new Sale
            {
                SaleId = sale.SaleId,
                SaleDate = sale.SaleDate,
                TotalAmount = sale.TotalAmount
            });

            database.SaveChanges();

            database.SaleItems.Add(new SaleItem
            {
                SaleId = sale.SaleId,
                ProductId = sale.Item.ProductId,
                ProductTitle = sale.Item.ProductTitle,
                QuantitySold = sale.Item.QuantitySold,
                UnitPrice = sale.Item.UnitPrice,
                ItemTotal = sale.Item.ItemTotal
            });

            Stock? stock = database.Stock.Find(sale.Item.ProductId);

            if (stock != null)
            {
                stock.QuantityInStock -= sale.Item.QuantitySold;
            }

            database.SaveChanges();
        }

    }
}