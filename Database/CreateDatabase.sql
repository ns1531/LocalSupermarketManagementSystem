IF DB_ID('LocalSupermarketManagementSystem') IS NULL
BEGIN
    CREATE DATABASE LocalSupermarketManagementSystem;
END
GO

USE LocalSupermarketManagementSystem;
GO

IF OBJECT_ID('SaleItems', 'U') IS NOT NULL DROP TABLE SaleItems;
IF OBJECT_ID('Sales', 'U') IS NOT NULL DROP TABLE Sales;
IF OBJECT_ID('Stock', 'U') IS NOT NULL DROP TABLE Stock;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Suppliers', 'U') IS NOT NULL DROP TABLE Suppliers;
IF OBJECT_ID('Categories', 'U') IS NOT NULL DROP TABLE Categories;
GO

CREATE TABLE Categories
(
    CategoryId NVARCHAR(20) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Suppliers
(
    SupplierId NVARCHAR(20) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    ContactNumber NVARCHAR(30) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE Products
(
    ProductId NVARCHAR(20) PRIMARY KEY,
    Barcode NVARCHAR(50) NOT NULL UNIQUE,
    Title NVARCHAR(150) NOT NULL,
    Brand NVARCHAR(100) NOT NULL,
    CategoryId NVARCHAR(20) NOT NULL,
    SupplierId NVARCHAR(20) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL CHECK (Price > 0),
    RestockDate DATE NOT NULL,

    CONSTRAINT FK_Products_Categories
        FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),

    CONSTRAINT FK_Products_Suppliers
        FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId)
);

CREATE TABLE Stock
(
    ProductId NVARCHAR(20) PRIMARY KEY,
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    LowStockThreshold INT NOT NULL CHECK (LowStockThreshold >= 0),

    CONSTRAINT FK_Stock_Products
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Sales
(
    SaleId NVARCHAR(20) PRIMARY KEY,
    SaleDate DATETIME2 NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL CHECK (TotalAmount >= 0)
);

CREATE TABLE SaleItems
(
    SaleItemId INT IDENTITY(1,1) PRIMARY KEY,
    SaleId NVARCHAR(20) NOT NULL,
    ProductId NVARCHAR(20) NOT NULL,
    ProductTitle NVARCHAR(150) NOT NULL,
    QuantitySold INT NOT NULL CHECK (QuantitySold > 0),
    UnitPrice DECIMAL(10, 2) NOT NULL CHECK (UnitPrice > 0),
    ItemTotal DECIMAL(10, 2) NOT NULL CHECK (ItemTotal >= 0),

    CONSTRAINT FK_SaleItems_Sales
        FOREIGN KEY (SaleId) REFERENCES Sales(SaleId),

    CONSTRAINT FK_SaleItems_Products
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
GO