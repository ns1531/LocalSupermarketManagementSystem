using System;

using SupermarketManagementSystem.ShopData;
using SupermarketManagementSystem.ShopDataStructures;
using SupermarketManagementSystem.ShopDatabase;

namespace SupermarketManagementSystem
{
    class Program
    {
        static void Main()
        {
            ProductCatalogueArray productCatalogue = ShopDataLoader.LoadProducts();
            BarcodeIndexTable barcodeIndex = CreateBarcodeIndex(productCatalogue);
            SupplierRecordArray supplierRecords = ShopDataLoader.LoadSuppliers();
            SaleRecordArray saleRecords = ShopDataLoader.LoadSales();

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("LOCAL SUPERMARKET MANAGEMENT SYSTEM");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Product Catalogue");
                Console.WriteLine("2. Supplier Records");
                Console.WriteLine("3. Product Search");
                Console.WriteLine("4. Stock Control");
                Console.WriteLine("5. Checkout & Sales Records");
                Console.WriteLine("6. Shop Reports");
                Console.WriteLine("7. Exit");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 7): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowProductCatalogueMenu(productCatalogue, barcodeIndex);
                        break;

                    case "2":
                        ShowSupplierRecordsMenu(supplierRecords);
                        break;

                    case "3":
                        ShowProductSearchMenu(productCatalogue, barcodeIndex);
                        break;

                    case "4":
                        ShowStockControlMenu(productCatalogue);
                        break;

                    case "5":
                        ShowCheckoutSalesMenu(productCatalogue, barcodeIndex, saleRecords);
                        break;

                    case "6":
                        ShowShopReportsMenu(productCatalogue, supplierRecords, saleRecords);
                        break;

                    case "7":
                        running = false;
                        Console.WriteLine("Exiting application.");
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 7.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowProductCatalogueMenu(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex)
        {
            bool inProductMenu = true;

            while (inProductMenu)
            {
                Console.Clear();

                Console.WriteLine("PRODUCT CATALOGUE");
                Console.WriteLine("=================");
                Console.WriteLine("1. View All Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ViewAllProducts(productCatalogue);
                        break;

                    case "2":
                        AddProductToCatalogue(productCatalogue, barcodeIndex);
                        break;

                    case "3":
                        UpdateProductInCatalogue(productCatalogue);
                        break;

                    case "4":
                        RemoveProductFromCatalogue(productCatalogue, barcodeIndex);
                        break;

                    case "5":
                        inProductMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 5.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowSupplierRecordsMenu(SupplierRecordArray supplierRecords)
        {
            bool inSupplierMenu = true;

            while (inSupplierMenu)
            {
                Console.Clear();

                Console.WriteLine("SUPPLIER RECORDS");
                Console.WriteLine("================");
                Console.WriteLine("1. View All Suppliers");
                Console.WriteLine("2. Add Supplier");
                Console.WriteLine("3. Update Supplier");
                Console.WriteLine("4. Remove Supplier");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ViewAllSuppliers(supplierRecords);
                        break;

                    case "2":
                        AddSupplierToRecords(supplierRecords);
                        break;

                    case "3":
                        UpdateSupplierInRecords(supplierRecords);
                        break;

                    case "4":
                        RemoveSupplierFromRecords(supplierRecords);
                        break;

                    case "5":
                        inSupplierMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 5.");
                        Pause();
                        break;
                }
            }
        }

        static void ViewAllSuppliers(SupplierRecordArray supplierRecords)
        {
            Console.Clear();

            Console.WriteLine("ALL SUPPLIERS");
            Console.WriteLine("=============");

            if (supplierRecords.Count == 0)
            {
                Console.WriteLine("No suppliers were found.");
                Pause();
                return;
            }

            for (int i = 0; i < supplierRecords.Count; i++)
            {
                Supplier? supplier = supplierRecords.GetSupplierAt(i);

                if (supplier != null)
                {
                    Console.WriteLine($"ID: {supplier.SupplierId}");
                    Console.WriteLine($"Name: {supplier.SupplierName}");
                    Console.WriteLine($"Contact Number: {supplier.ContactNumber}");
                    Console.WriteLine($"Email: {supplier.Email}");
                    Console.WriteLine("-----------------------------------");
                }
            }

            Pause();
        }

        static void AddSupplierToRecords(SupplierRecordArray supplierRecords)
        {
            Console.Clear();

            Console.WriteLine("ADD SUPPLIER");
            Console.WriteLine("============");
            Console.Write("Enter supplier ID: ");

            string supplierId = Console.ReadLine() ?? "";

            if (supplierRecords.SearchBySupplierId(supplierId) != null)
            {
                Console.WriteLine("Error! A supplier with this ID already exists.");
                Pause();
                return;
            }

            Console.Write("Enter supplier name: ");
            string supplierName = Console.ReadLine() ?? "";

            Console.Write("Enter contact number: ");
            string contactNumber = Console.ReadLine() ?? "";

            Console.Write("Enter email: ");
            string email = Console.ReadLine() ?? "";

            Supplier supplier = new Supplier
            {
                SupplierId = supplierId,
                SupplierName = supplierName,
                ContactNumber = contactNumber,
                Email = email
            };

            bool saved = ShopDataLoader.SaveSupplier(supplier);

            if (!saved)
            {
                Console.WriteLine();
                Console.WriteLine("Supplier could not be saved to the database.");
                Pause();
                return;
            }

            supplierRecords.AddSupplier(supplier);

            Console.WriteLine();
            Console.WriteLine("Supplier was added successfully.");
            Pause();
        }

        static void UpdateSupplierInRecords(SupplierRecordArray supplierRecords)
        {
            Console.Clear();

            Console.WriteLine("UPDATE SUPPLIER");
            Console.WriteLine("===============");
            Console.Write("Enter supplier ID to update: ");

            string supplierId = Console.ReadLine() ?? "";

            Supplier? supplier = supplierRecords.SearchBySupplierId(supplierId);

            if (supplier == null)
            {
                Console.WriteLine("No supplier with this ID was found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Current supplier details:");
            Console.WriteLine($"Name: {supplier.SupplierName}");
            Console.WriteLine($"Contact Number: {supplier.ContactNumber}");
            Console.WriteLine($"Email: {supplier.Email}");
            Console.WriteLine();

            Console.Write("Enter new supplier name: ");
            supplier.SupplierName = Console.ReadLine() ?? "";

            Console.Write("Enter new contact number: ");
            supplier.ContactNumber = Console.ReadLine() ?? "";

            Console.Write("Enter new email: ");
            supplier.Email = Console.ReadLine() ?? "";

            bool updated = ShopDataLoader.UpdateSupplier(supplier);

            if (!updated)
            {
                Console.WriteLine();
                Console.WriteLine("Supplier could not be updated in the database.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Supplier was updated successfully.");
            Pause();
        }

        static void RemoveSupplierFromRecords(SupplierRecordArray supplierRecords)
        {
            Console.Clear();

            Console.WriteLine("REMOVE SUPPLIER");
            Console.WriteLine("===============");
            Console.Write("Enter supplier ID to remove: ");

            string supplierId = Console.ReadLine() ?? "";

            bool removedFromDatabase = ShopDataLoader.RemoveSupplier(supplierId);

            if (!removedFromDatabase)
            {
                Console.WriteLine();
                Console.WriteLine("Supplier could not be removed from the database.");
                Console.WriteLine("It may not exist, or it may already be linked to product records.");
                Pause();
                return;
            }

            bool removedFromRecords = supplierRecords.RemoveSupplier(supplierId);

            Console.WriteLine();

            if (removedFromRecords)
            {
                Console.WriteLine("Supplier was removed successfully.");
            }
            else
            {
                Console.WriteLine("Supplier was removed from the database, but was not found in the current records.");
            }

            Pause();
        }

        static void AddProductToCatalogue(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex)
        {
            Console.Clear();

            Console.WriteLine("ADD PRODUCT");
            Console.WriteLine("===========");
            Console.Write("Enter product ID: ");
            string productId = Console.ReadLine() ?? "";

            if (productCatalogue.SearchByProductId(productId) != null)
            {
                Console.WriteLine("Error! A product with this ID already exists.");
                Pause();
                return;
            }

            Console.Write("Enter barcode: ");
            string barcode = Console.ReadLine() ?? "";

            if (productCatalogue.BarcodeExists(barcode))
            {
                Console.WriteLine("Error! A product with this barcode already exists.");
                Pause();
                return;
            }

            Console.Write("Enter title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter brand: ");
            string brand = Console.ReadLine() ?? "";

            Console.Write("Enter category ID: ");
            string categoryId = Console.ReadLine() ?? "";

            Console.Write("Enter supplier ID: ");
            string supplierId = Console.ReadLine() ?? "";

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter quantity in stock: ");
            int quantityInStock = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter low stock threshold: ");
            int lowStockThreshold = int.Parse(Console.ReadLine() ?? "0");

            Product product = new Product
            {
                ProductId = productId,
                Barcode = barcode,
                Title = title,
                Brand = brand,
                CategoryId = categoryId,
                SupplierId = supplierId,
                Price = price,
                QuantityInStock = quantityInStock,
                LowStockThreshold = lowStockThreshold,
                RestockDate = DateTime.Today.AddDays(7)
            };

            bool saved = ShopDataLoader.SaveProduct(product);

            if (!saved)
            {
                Console.WriteLine();
                Console.WriteLine("Product could not be saved to the database.");
                Pause();
                return;
            }

            productCatalogue.AddProduct(product);
            barcodeIndex.AddProduct(product);

            Console.WriteLine();
            Console.WriteLine("Product was added successfully.");
            Pause();
        }

        static void UpdateProductInCatalogue(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("UPDATE PRODUCT");
            Console.WriteLine("==============");
            Console.Write("Enter product ID to update: ");

            string productId = Console.ReadLine() ?? "";

            Product? product = productCatalogue.SearchByProductId(productId);

            if (product == null)
            {
                Console.WriteLine("No product with this ID was found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Current product details:");
            Console.WriteLine($"Title: {product.Title}");
            Console.WriteLine($"Price: Rs {product.Price}");
            Console.WriteLine($"Quantity: {product.QuantityInStock}");
            Console.WriteLine();

            Console.Write("Enter new title: ");
            product.Title = Console.ReadLine() ?? "";

            Console.Write("Enter new price: ");
            product.Price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter new quantity in stock: ");
            product.QuantityInStock = int.Parse(Console.ReadLine() ?? "0");

            bool updated = ShopDataLoader.UpdateProduct(product);

            if (!updated)
            {
                Console.WriteLine();
                Console.WriteLine("Product could not be updated in the database.");
                Pause();
                return;
            }
        }

        static void RemoveProductFromCatalogue(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex)
        {
            Console.Clear();

            Console.WriteLine("REMOVE PRODUCT");
            Console.WriteLine("==============");
            Console.Write("Enter product ID to remove: ");

            string productId = Console.ReadLine() ?? "";

            bool removedFromDatabase = ShopDataLoader.RemoveProduct(productId);

            if (!removedFromDatabase)
            {
                Console.WriteLine();
                Console.WriteLine("Product could not be removed from the database.");
                Console.WriteLine("It may not exist, or it may already be linked to sales records.");
                Pause();
                return;
            }

            bool removedFromCatalogue = productCatalogue.RemoveProduct(productId);

            if (removedFromCatalogue)
            {
                RebuildBarcodeIndex(productCatalogue, barcodeIndex);
            }

            Console.WriteLine();

            if (removedFromCatalogue)
            {
                Console.WriteLine("Product was removed successfully.");
            }
            else
            {
                Console.WriteLine("Product was removed from the database, but was not found in the current catalogue.");
            }

            Pause();
        }

        static void ShowProductSearchMenu(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex)
        {
            bool inSearchMenu = true;

            while (inSearchMenu)
            {
                Console.Clear();

                Console.WriteLine("PRODUCT SEARCH");
                Console.WriteLine("==============");
                Console.WriteLine("1. Search product by name");
                Console.WriteLine("2. Search product by barcode");
                Console.WriteLine("3. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 or 2): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        SearchProductByName(productCatalogue);
                        break;

                    case "2":
                        SearchProductByBarcode(barcodeIndex);
                        break;

                    case "3":
                        inSearchMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 3.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowStockControlMenu(ProductCatalogueArray productCatalogue)
        {
            bool inStockMenu = true;

            while (inStockMenu)
            {
                Console.Clear();

                Console.WriteLine("STOCK CONTROL");
                Console.WriteLine("=============");
                Console.WriteLine("1. View Stock Status");
                Console.WriteLine("2. Update Stock Quantity");
                Console.WriteLine("3. View Low-Stock Alerts");
                Console.WriteLine("4. View Restocking Needs");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ViewStockStatus(productCatalogue);
                        break;

                    case "2":
                        UpdateStockQuantity(productCatalogue);
                        break;

                    case "3":
                        ViewLowStockAlerts(productCatalogue);
                        break;

                    case "4":
                        ViewRestockingNeeds(productCatalogue);
                        break;

                    case "5":
                        inStockMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 4.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowCheckoutSalesMenu(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex, SaleRecordArray saleRecords)
        {
            bool inSalesMenu = true;

            while (inSalesMenu)
            {
                Console.Clear();

                Console.WriteLine("CHECKOUT & SALES RECORDS");
                Console.WriteLine("========================");
                Console.WriteLine("1. Record Sale");
                Console.WriteLine("2. View Sales Records");
                Console.WriteLine("3. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 3): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        RecordSale(barcodeIndex, saleRecords);
                        break;

                    case "2":
                        ViewSalesRecords(saleRecords);
                        break;

                    case "3":
                        inSalesMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 3.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowShopReportsMenu(ProductCatalogueArray productCatalogue, SupplierRecordArray supplierRecords, SaleRecordArray saleRecords)
        {
            bool inReportsMenu = true;

            while (inReportsMenu)
            {
                Console.Clear();

                Console.WriteLine("SHOP REPORTS");
                Console.WriteLine("============");
                Console.WriteLine("1. Low Stock Report");
                Console.WriteLine("2. Products by Category");
                Console.WriteLine("3. Supplier Stock List");
                Console.WriteLine("4. Sales by Product");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowLowStockReport(productCatalogue);
                        break;

                    case "2":
                        ShowProductsByCategoryReport(productCatalogue);
                        break;

                    case "3":
                        ShowSupplierStockList(productCatalogue, supplierRecords);
                        break;

                    case "4":
                        ShowSalesByProductReport(saleRecords);
                        break;

                    case "5":
                        inReportsMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 5.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowLowStockReport(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("LOW STOCK REPORT");
            Console.WriteLine("================");

            bool lowStockFound = false;

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null && product.QuantityInStock <= product.LowStockThreshold)
                {
                    Console.WriteLine($"ID: {product.ProductId}");
                    Console.WriteLine($"Title: {product.Title}");
                    Console.WriteLine($"Quantity: {product.QuantityInStock}");
                    Console.WriteLine($"Low Stock Threshold: {product.LowStockThreshold}");
                    Console.WriteLine($"Status: {product.StockStatus}");
                    Console.WriteLine("-----------------------------------");

                    lowStockFound = true;
                }
            }

            if (!lowStockFound)
            {
                Console.WriteLine("No low stock products were found.");
            }

            Pause();
        }

        static void ShowProductsByCategoryReport(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("PRODUCTS BY CATEGORY");
            Console.WriteLine("====================");
            Console.Write("Enter category ID: ");

            string categoryId = Console.ReadLine() ?? "";

            Console.WriteLine();

            bool productsFound = false;

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null && product.CategoryId == categoryId)
                {
                    DisplayProductDetails(product);
                    Console.WriteLine("-----------------------------------");

                    productsFound = true;
                }
            }

            if (!productsFound)
            {
                Console.WriteLine("No products were found for this category.");
            }

            Pause();
        }

        static void ShowSupplierStockList(ProductCatalogueArray productCatalogue, SupplierRecordArray supplierRecords)
        {
            Console.Clear();

            Console.WriteLine("SUPPLIER STOCK LIST");
            Console.WriteLine("===================");
            Console.Write("Enter supplier ID: ");

            string supplierId = Console.ReadLine() ?? "";

            Supplier? supplier = supplierRecords.SearchBySupplierId(supplierId);

            Console.WriteLine();

            if (supplier == null)
            {
                Console.WriteLine("No supplier with this ID was found.");
                Pause();
                return;
            }

            Console.WriteLine($"Supplier: {supplier.SupplierName}");
            Console.WriteLine($"Contact Number: {supplier.ContactNumber}");
            Console.WriteLine($"Email: {supplier.Email}");
            Console.WriteLine();

            bool productsFound = false;

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null && product.SupplierId == supplierId)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}");
                    Console.WriteLine($"Title: {product.Title}");
                    Console.WriteLine($"Quantity: {product.QuantityInStock}");
                    Console.WriteLine($"Status: {product.StockStatus}");
                    Console.WriteLine("-----------------------------------");

                    productsFound = true;
                }
            }

            if (!productsFound)
            {
                Console.WriteLine("No products were found for this supplier.");
            }

            Pause();
        }

        static void ShowSalesByProductReport(SaleRecordArray saleRecords)
        {
            Console.Clear();

            Console.WriteLine("SALES BY PRODUCT");
            Console.WriteLine("================");
            Console.Write("Enter product ID: ");

            string productId = Console.ReadLine() ?? "";

            Console.WriteLine();

            int totalQuantitySold = 0;
            decimal totalSalesAmount = 0;
            bool salesFound = false;

            for (int i = 0; i < saleRecords.Count; i++)
            {
                Sale? sale = saleRecords.GetSaleAt(i);

                if (sale != null && sale.Item.ProductId == productId)
                {
                    Console.WriteLine($"Sale ID: {sale.SaleId}");
                    Console.WriteLine($"Date: {sale.SaleDate:dd/MM/yyyy HH:mm}");
                    Console.WriteLine($"Product: {sale.Item.ProductTitle}");
                    Console.WriteLine($"Quantity Sold: {sale.Item.QuantitySold}");
                    Console.WriteLine($"Item Total: Rs {sale.Item.ItemTotal}");
                    Console.WriteLine("-----------------------------------");

                    totalQuantitySold += sale.Item.QuantitySold;
                    totalSalesAmount += sale.Item.ItemTotal;
                    salesFound = true;
                }
            }

            if (!salesFound)
            {
                Console.WriteLine("No sales records were found for this product.");
            }
            else
            {
                Console.WriteLine($"Total Quantity Sold: {totalQuantitySold}");
                Console.WriteLine($"Total Sales Amount: Rs {totalSalesAmount}");
            }

            Pause();
        }

        static void RecordSale(BarcodeIndexTable barcodeIndex, SaleRecordArray saleRecords)
        {
            Console.Clear();

            Console.WriteLine("RECORD SALE");
            Console.WriteLine("===========");
            Console.Write("Enter product barcode: ");

            string barcode = Console.ReadLine() ?? "";

            Product? product = barcodeIndex.SearchByBarcode(barcode);

            if (product == null)
            {
                Console.WriteLine("No product with this barcode was found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Product: {product.Title}");
            Console.WriteLine($"Price: Rs {product.Price}");
            Console.WriteLine($"Available Quantity: {product.QuantityInStock}");
            Console.Write("Enter quantity sold: ");

            int quantitySold = int.Parse(Console.ReadLine() ?? "0");

            if (quantitySold > product.QuantityInStock)
            {
                Console.WriteLine("Not enough stock available.");
                Pause();
                return;
            }

            decimal totalAmount = product.Price * quantitySold;

            SaleItem saleItem = new SaleItem
            {
                SaleId = "SALE" + (saleRecords.Count + 1).ToString("000"),
                ProductId = product.ProductId,
                ProductTitle = product.Title,
                QuantitySold = quantitySold,
                UnitPrice = product.Price,
                ItemTotal = totalAmount
            };

            Sale sale = new Sale
            {
                SaleId = saleItem.SaleId,
                SaleDate = DateTime.Now,
                TotalAmount = totalAmount,
                Item = saleItem
            };

            product.QuantityInStock -= quantitySold;
            saleRecords.AddSale(sale);
            ShopDataLoader.SaveSale(sale);

            Console.WriteLine();
            Console.WriteLine("Sale was recorded successfully.");
            Console.WriteLine($"Sale ID: {sale.SaleId}");
            Console.WriteLine($"Total Amount: Rs {sale.TotalAmount}");
            Pause();
        }

        static void ViewSalesRecords(SaleRecordArray saleRecords)
        {
            Console.Clear();

            Console.WriteLine("SALES RECORDS");
            Console.WriteLine("=============");

            if (saleRecords.Count == 0)
            {
                Console.WriteLine("No sales records were found.");
                Pause();
                return;
            }

            for (int i = 0; i < saleRecords.Count; i++)
            {
                Sale? sale = saleRecords.GetSaleAt(i);

                if (sale != null)
                {
                    Console.WriteLine($"Sale ID: {sale.SaleId}");
                    Console.WriteLine($"Date: {sale.SaleDate:dd/MM/yyyy HH:mm}");
                    Console.WriteLine($"Product ID: {sale.Item.ProductId}");
                    Console.WriteLine($"Product: {sale.Item.ProductTitle}");
                    Console.WriteLine($"Quantity Sold: {sale.Item.QuantitySold}");
                    Console.WriteLine($"Unit Price: Rs {sale.Item.UnitPrice}");
                    Console.WriteLine($"Item Total: Rs {sale.Item.ItemTotal}");
                    Console.WriteLine($"Sale Total: Rs {sale.TotalAmount}");
                    Console.WriteLine("-----------------------------------");
                }
            }

            Pause();
        }

        static void ViewStockStatus(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("STOCK STATUS");
            Console.WriteLine("============");

            if (productCatalogue.Count == 0)
            {
                Console.WriteLine("No products were found.");
                Pause();
                return;
            }

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null)
                {
                    Console.WriteLine($"ID: {product.ProductId}");
                    Console.WriteLine($"Title: {product.Title}");
                    Console.WriteLine($"Quantity: {product.QuantityInStock}");
                    Console.WriteLine($"Low Stock Threshold: {product.LowStockThreshold}");
                    Console.WriteLine($"Status: {product.StockStatus}");
                    Console.WriteLine("-----------------------------------");
                }
            }

            Pause();
        }

        static void UpdateStockQuantity(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("UPDATE STOCK QUANTITY");
            Console.WriteLine("=====================");
            Console.Write("Enter product ID: ");

            string productId = Console.ReadLine() ?? "";

            Product? product = productCatalogue.SearchByProductId(productId);

            if (product == null)
            {
                Console.WriteLine("No product with this ID was found.");
                Pause();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Product: {product.Title}");
            Console.WriteLine($"Current Quantity: {product.QuantityInStock}");
            Console.Write("Enter new quantity: ");

            int newQuantity = int.Parse(Console.ReadLine() ?? "0");

            product.QuantityInStock = newQuantity;
            ShopDataLoader.UpdateStockQuantity(product.ProductId, newQuantity);

            Console.WriteLine();
            Console.WriteLine("Stock quantity was updated successfully.");
            Pause();
        }

        static void ViewLowStockAlerts(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("LOW-STOCK ALERTS");
            Console.WriteLine("================");

            bool lowStockFound = false;

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null && product.QuantityInStock <= product.LowStockThreshold)
                {
                    Console.WriteLine($"ID: {product.ProductId}");
                    Console.WriteLine($"Title: {product.Title}");
                    Console.WriteLine($"Quantity: {product.QuantityInStock}");
                    Console.WriteLine($"Low Stock Threshold: {product.LowStockThreshold}");
                    Console.WriteLine($"Status: {product.StockStatus}");
                    Console.WriteLine("-----------------------------------");

                    lowStockFound = true;
                }
            }

            if (!lowStockFound)
            {
                Console.WriteLine("No low-stock products were found.");
            }

            Pause();
        }

        static void ViewRestockingNeeds(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("RESTOCKING NEEDS");
            Console.WriteLine("================");

            bool restockingNeeded = false;

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null && product.QuantityInStock <= product.LowStockThreshold)
                {
                    Console.WriteLine($"ID: {product.ProductId}");
                    Console.WriteLine($"Title: {product.Title}");
                    Console.WriteLine($"Quantity: {product.QuantityInStock}");
                    Console.WriteLine($"Low Stock Threshold: {product.LowStockThreshold}");
                    Console.WriteLine($"Restock Date: {product.RestockDate:dd/MM/yyyy}");
                    Console.WriteLine("-----------------------------------");

                    restockingNeeded = true;
                }
            }

            if (!restockingNeeded)
            {
                Console.WriteLine("No restocking needs were found.");
            }

            Pause();
        }

        static void SearchProductByName(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("SEARCH BY PRODUCT NAME");
            Console.WriteLine("======================");
            Console.Write("Enter product name: ");

            string productName = Console.ReadLine() ?? "";

            Product? product = productCatalogue.SearchByName(productName);

            Console.WriteLine();

            if (product == null)
            {
                Console.WriteLine("No matching product was found.");
            }
            else
            {
                Console.WriteLine("Product details:");
                DisplayProductDetails(product);
            }

            Pause();
        }

        static void SearchProductByBarcode(BarcodeIndexTable barcodeIndex)
        {
            Console.Clear();

            Console.WriteLine("SEARCH BY BARCODE");
            Console.WriteLine("=================");
            Console.Write("Enter product barcode: ");

            string barcode = Console.ReadLine() ?? "";

            Product? product = barcodeIndex.SearchByBarcode(barcode);

            Console.WriteLine();

            if (product == null)
            {
                Console.WriteLine("No matching product was found.");
            }
            else
            {
                Console.WriteLine("Product details:");
                DisplayProductDetails(product);
            }

            Pause();
        }

        static void ViewAllProducts(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("ALL PRODUCTS");
            Console.WriteLine("============");

            if (productCatalogue.Count == 0)
            {
                Console.WriteLine("No products were found.");
                Pause();
                return;
            }

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null)
                {
                    DisplayProductDetails(product);
                    Console.WriteLine("-----------------------------------");
                }
            }

            Pause();
        }

        static void DisplayProductDetails(Product product)
        {
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Title: {product.Title}");
            Console.WriteLine($"Barcode: {product.Barcode}");
            Console.WriteLine($"Brand: {product.Brand}");
            Console.WriteLine($"Category ID: {product.CategoryId}");
            Console.WriteLine($"Supplier ID: {product.SupplierId}");
            Console.WriteLine($"Price: Rs {product.Price}");
            Console.WriteLine($"Quantity: {product.QuantityInStock}");
            Console.WriteLine($"Status: {product.StockStatus}");
            Console.WriteLine($"Restock Date: {product.RestockDate:dd/MM/yyyy}");
        }

        static BarcodeIndexTable CreateBarcodeIndex(ProductCatalogueArray productCatalogue)
        {
            BarcodeIndexTable barcodeIndex = new BarcodeIndexTable();

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null)
                {
                    barcodeIndex.AddProduct(product);
                }
            }

            return barcodeIndex;
        }

        static void RebuildBarcodeIndex(ProductCatalogueArray productCatalogue, BarcodeIndexTable barcodeIndex)
        {
            barcodeIndex.Clear();

            for (int i = 0; i < productCatalogue.Count; i++)
            {
                Product? product = productCatalogue.GetProductAt(i);

                if (product != null)
                {
                    barcodeIndex.AddProduct(product);
                }
            }
        }

        static void ShowSectionPlaceholder(string sectionName)
        {
            Console.Clear();
            Console.WriteLine(sectionName.ToUpper());
            Console.WriteLine("==============================");
            Console.WriteLine("This section is under development.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
    }
}