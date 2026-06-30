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

        public static bool SaveProduct(Product product)
        {
            using ShopDbContext database = new ShopDbContext();

            if (database.Products.Find(product.ProductId) != null)
            {
                return false;
            }

            if (database.Products.Any(existingProduct => existingProduct.Barcode == product.Barcode))
            {
                return false;
            }

            if (database.Categories.Find(product.CategoryId) == null)
            {
                return false;
            }

            if (database.Suppliers.Find(product.SupplierId) == null)
            {
                return false;
            }

            database.Products.Add(new Product
            {
                ProductId = product.ProductId,
                Barcode = product.Barcode,
                Title = product.Title,
                Brand = product.Brand,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                Price = product.Price,
                RestockDate = product.RestockDate
            });

            database.Stock.Add(new Stock
            {
                ProductId = product.ProductId,
                QuantityInStock = product.QuantityInStock,
                LowStockThreshold = product.LowStockThreshold
            });

            database.SaveChanges();
            return true;
        }

        public static bool UpdateProduct(Product product)
        {
            using ShopDbContext database = new ShopDbContext();

            Product? databaseProduct = database.Products.Find(product.ProductId);
            Stock? databaseStock = database.Stock.Find(product.ProductId);

            if (databaseProduct == null || databaseStock == null)
            {
                return false;
            }

            databaseProduct.Title = product.Title;
            databaseProduct.Brand = product.Brand;
            databaseProduct.CategoryId = product.CategoryId;
            databaseProduct.SupplierId = product.SupplierId;
            databaseProduct.Price = product.Price;
            databaseProduct.RestockDate = product.RestockDate;

            databaseStock.QuantityInStock = product.QuantityInStock;
            databaseStock.LowStockThreshold = product.LowStockThreshold;

            database.SaveChanges();
            return true;
        }

        public static bool RemoveProduct(string productId)
        {
            using ShopDbContext database = new ShopDbContext();

            bool productHasSales = database.SaleItems.Any(saleItem => saleItem.ProductId == productId);

            if (productHasSales)
            {
                return false;
            }

            Stock? stock = database.Stock.Find(productId);

            if (stock != null)
            {
                database.Stock.Remove(stock);
                database.SaveChanges();
            }

            Product? product = database.Products.Find(productId);

            if (product == null)
            {
                return false;
            }

            database.Products.Remove(product);
            database.SaveChanges();

            return true;
        }

        public static bool SaveSupplier(Supplier supplier)
        {
            using ShopDbContext database = new ShopDbContext();

            if (database.Suppliers.Find(supplier.SupplierId) != null)
            {
                return false;
            }

            database.Suppliers.Add(new Supplier
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactNumber = supplier.ContactNumber,
                Email = supplier.Email
            });

            database.SaveChanges();
            return true;
        }

        public static bool UpdateSupplier(Supplier supplier)
        {
            using ShopDbContext database = new ShopDbContext();

            Supplier? databaseSupplier = database.Suppliers.Find(supplier.SupplierId);

            if (databaseSupplier == null)
            {
                return false;
            }

            databaseSupplier.SupplierName = supplier.SupplierName;
            databaseSupplier.ContactNumber = supplier.ContactNumber;
            databaseSupplier.Email = supplier.Email;

            database.SaveChanges();
            return true;
        }

        public static bool RemoveSupplier(string supplierId)
        {
            using ShopDbContext database = new ShopDbContext();

            bool supplierHasProducts = database.Products.Any(product => product.SupplierId == supplierId);

            if (supplierHasProducts)
            {
                return false;
            }

            Supplier? supplier = database.Suppliers.Find(supplierId);

            if (supplier == null)
            {
                return false;
            }

            database.Suppliers.Remove(supplier);
            database.SaveChanges();

            return true;
        }

    }
}