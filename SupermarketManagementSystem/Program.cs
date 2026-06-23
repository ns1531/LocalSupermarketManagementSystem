using System;
using SupermarketManagementSystem.ShopData;
using SupermarketManagementSystem.ShopDataStructures;

namespace SupermarketManagementSystem
{
    class Program
    {
        static void Main()
        {
            ProductCatalogueArray productCatalogue = CreateSampleProducts();
            BarcodeIndexTable barcodeIndex = CreateBarcodeIndex(productCatalogue);

            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("LOCAL SUPERMRKET MANAGEMENT SYSTEM");
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
                        ShowSectionPlaceholder("Supplier Records");
                        break;

                    case "3":
                        ShowProductSearchMenu(productCatalogue, barcodeIndex);
                        break;

                    case "4":
                        ShowSectionPlaceholder("Stock Control");
                        break;

                    case "5":
                        ShowSectionPlaceholder("Checkout & Sales Records");
                        break;

                    case "6":
                        ShowSectionPlaceholder("Shop Reports");
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
                        RemoveProductFromCatalogue(productCatalogue);
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

            Console.WriteLine();
            Console.WriteLine("Product was updated successfully.");
            Pause();
        }

        static void RemoveProductFromCatalogue(ProductCatalogueArray productCatalogue)
        {
            Console.Clear();

            Console.WriteLine("REMOVE PRODUCT");
            Console.WriteLine("==============");
            Console.Write("Enter product ID to remove: ");

            string productId = Console.ReadLine() ?? "";

            bool removed = productCatalogue.RemoveProduct(productId);

            Console.WriteLine();

            if (removed)
            {
                Console.WriteLine("Product was removed successfully.");
            }
            else
            {
                Console.WriteLine("No product with this ID was found.");
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
                    Console.WriteLine("-----------------------------------");
                }
            }

            Pause();
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

        static ProductCatalogueArray CreateSampleProducts()
        {
            ProductCatalogueArray productCatalogue = new ProductCatalogueArray();

            productCatalogue.AddProduct(new Product
            {
                ProductId = "P001",
                Barcode = "6091000000011",
                Title = "Phoenix Drink",
                Brand = "Phoenix",
                CategoryId = "C001",
                SupplierId = "S001",
                Price = 80,
                QuantityInStock = 24,
                LowStockThreshold = 5,
                RestockDate = DateTime.Today.AddDays(7)
            });

            productCatalogue.AddProduct(new Product
            {
                ProductId = "P002",
                Barcode = "6091000000028",
                Title = "Apollo Instant Noodles",
                Brand = "Apollo",
                CategoryId = "C002",
                SupplierId = "S002",
                Price = 10,
                QuantityInStock = 12,
                LowStockThreshold = 6,
                RestockDate = DateTime.Today.AddDays(5)
            });

            productCatalogue.AddProduct(new Product
            {
                ProductId = "P003",
                Barcode = "6091000000035",
                Title = "Bread",
                Brand = "Bakery",
                CategoryId = "C003",
                SupplierId = "S003",
                Price = 7,
                QuantityInStock = 8,
                LowStockThreshold = 4,
                RestockDate = DateTime.Today.AddDays(2)
            });

            productCatalogue.AddProduct(new Product
            {
                ProductId = "P004",
                Barcode = "6091000000042",
                Title = "Doritos Nacho Cheese Flavoured Corn Chips",
                Brand = "Frito-Lay",
                CategoryId = "C004",
                SupplierId = "S004",
                Price = 45,
                QuantityInStock = 10,
                LowStockThreshold = 3,
                RestockDate = DateTime.Today.AddDays(10)
            });

            productCatalogue.AddProduct(new Product
            {
                ProductId = "P005",
                Barcode = "6091000000059",
                Title = "Brown Sugar",
                Brand = "Dina",
                CategoryId = "C004",
                SupplierId = "S004",
                Price = 90,
                QuantityInStock = 15,
                LowStockThreshold = 5,
                RestockDate = DateTime.Today.AddDays(8)
            });

            return productCatalogue;
        }

        static void ShowSectionPlaceholder(string sectionName)
        {
            Console.Clear();
            Console.WriteLine(sectionName.ToUpper());
            Console.WriteLine("==============================");
            Console.WriteLine("Test 123");
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