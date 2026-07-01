using SupermarketManagementSystem.ShopData;
using SupermarketManagementSystem.ShopDataStructures;

namespace SupermarketManagementSystem.Tests
{
    public class DataStructureTests
    {
        [Fact]
        public void ProductCatalogueArray_ShouldAddAndFindProductById()
        {
            ProductCatalogueArray catalogue = new ProductCatalogueArray();

            Product product = new Product
            {
                ProductId = "P100",
                Barcode = "B100",
                Title = "Test Product",
                Brand = "Test Brand",
                CategoryId = "C001",
                SupplierId = "S001",
                Price = 25,
                QuantityInStock = 10,
                LowStockThreshold = 3,
                RestockDate = DateTime.Today
            };

            catalogue.AddProduct(product);

            Product? foundProduct = catalogue.SearchByProductId("P100");

            Assert.NotNull(foundProduct);
            Assert.Equal("Test Product", foundProduct.Title);
        }

        [Fact]
        public void ProductCatalogueArray_ShouldFindProductByName()
        {
            ProductCatalogueArray catalogue = new ProductCatalogueArray();

            Product product = new Product
            {
                ProductId = "P103",
                Barcode = "B103",
                Title = "Chocolate Biscuit",
                Brand = "Test Brand",
                CategoryId = "C004",
                SupplierId = "S004",
                Price = 30,
                QuantityInStock = 12,
                LowStockThreshold = 4,
                RestockDate = DateTime.Today
            };

            catalogue.AddProduct(product);

            Product? foundProduct = catalogue.SearchByName("Chocolate");

            Assert.NotNull(foundProduct);
            Assert.Equal("Chocolate Biscuit", foundProduct.Title);
        }

        [Fact]
        public void ProductCatalogueArray_ShouldRemoveProduct()
        {
            ProductCatalogueArray catalogue = new ProductCatalogueArray();

            Product product = new Product
            {
                ProductId = "P101",
                Barcode = "B101",
                Title = "Product To Remove",
                Brand = "Test Brand",
                CategoryId = "C001",
                SupplierId = "S001",
                Price = 20,
                QuantityInStock = 5,
                LowStockThreshold = 2,
                RestockDate = DateTime.Today
            };

            catalogue.AddProduct(product);

            bool removed = catalogue.RemoveProduct("P101");
            Product? foundProduct = catalogue.SearchByProductId("P101");

            Assert.True(removed);
            Assert.Null(foundProduct);
        }

        [Fact]
        public void ProductCatalogueArray_ShouldReturnFalseWhenRemovingMissingProduct()
        {
            ProductCatalogueArray catalogue = new ProductCatalogueArray();

            bool removed = catalogue.RemoveProduct("P999");

            Assert.False(removed);
        }

        [Fact]
        public void BarcodeIndexTable_ShouldFindProductByBarcode()
        {
            BarcodeIndexTable barcodeIndex = new BarcodeIndexTable();

            Product product = new Product
            {
                ProductId = "P102",
                Barcode = "B102",
                Title = "Barcode Product",
                Brand = "Test Brand",
                CategoryId = "C001",
                SupplierId = "S001",
                Price = 15,
                QuantityInStock = 8,
                LowStockThreshold = 2,
                RestockDate = DateTime.Today
            };

            barcodeIndex.AddProduct(product);

            Product? foundProduct = barcodeIndex.SearchByBarcode("B102");

            Assert.NotNull(foundProduct);
            Assert.Equal("P102", foundProduct.ProductId);
        }

        [Fact]
        public void BarcodeIndexTable_ShouldReturnNullForMissingBarcode()
        {
            BarcodeIndexTable barcodeIndex = new BarcodeIndexTable();

            Product? foundProduct = barcodeIndex.SearchByBarcode("B999");

            Assert.Null(foundProduct);
        }

        [Fact]
        public void SupplierRecordArray_ShouldAddAndFindSupplier()
        {
            SupplierRecordArray supplierRecords = new SupplierRecordArray();

            Supplier supplier = new Supplier
            {
                SupplierId = "S100",
                SupplierName = "Test Supplier",
                ContactNumber = "1234567",
                Email = "test@supplier.com"
            };

            supplierRecords.AddSupplier(supplier);

            Supplier? foundSupplier = supplierRecords.SearchBySupplierId("S100");

            Assert.NotNull(foundSupplier);
            Assert.Equal("Test Supplier", foundSupplier.SupplierName);
        }

        [Fact]
        public void SupplierRecordArray_ShouldRemoveSupplier()
        {
            SupplierRecordArray supplierRecords = new SupplierRecordArray();

            Supplier supplier = new Supplier
            {
                SupplierId = "S101",
                SupplierName = "Supplier To Remove",
                ContactNumber = "7654321",
                Email = "remove@supplier.com"
            };

            supplierRecords.AddSupplier(supplier);

            bool removed = supplierRecords.RemoveSupplier("S101");
            Supplier? foundSupplier = supplierRecords.SearchBySupplierId("S101");

            Assert.True(removed);
            Assert.Null(foundSupplier);
        }

        [Fact]
        public void SupplierRecordArray_ShouldReturnFalseWhenRemovingMissingSupplier()
        {
            SupplierRecordArray supplierRecords = new SupplierRecordArray();

            bool removed = supplierRecords.RemoveSupplier("S999");

            Assert.False(removed);
        }

        [Fact]
        public void SaleRecordArray_ShouldAddAndRetrieveSale()
        {
            SaleRecordArray saleRecords = new SaleRecordArray();

            Sale sale = new Sale
            {
                SaleId = "SALE100",
                SaleDate = DateTime.Now,
                TotalAmount = 50,
                Item = new SaleItem
                {
                    SaleId = "SALE100",
                    ProductId = "P100",
                    ProductTitle = "Test Product",
                    QuantitySold = 2,
                    UnitPrice = 25,
                    ItemTotal = 50
                }
            };

            saleRecords.AddSale(sale);

            Sale? foundSale = saleRecords.GetSaleAt(0);

            Assert.NotNull(foundSale);
            Assert.Equal("SALE100", foundSale.SaleId);
            Assert.Equal(50, foundSale.TotalAmount);
        }
    }
}