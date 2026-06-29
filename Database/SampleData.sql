USE LocalSupermarketManagementSystem;
GO

INSERT INTO Categories (CategoryId, CategoryName)
VALUES
('C001', 'Beverages'),
('C002', 'Instant Foods'),
('C003', 'Bakery'),
('C004', 'Snacks and Groceries');

INSERT INTO Suppliers (SupplierId, SupplierName, ContactNumber, Email)
VALUES
('S001', 'Phoenix Beverages Ltd', '2123456', 'orders@phoenixbeverages.mu'),
('S002', 'Apollo Foods Ltd', '2345678', 'sales@apollofoods.mu'),
('S003', 'Local Bakery Supplier', '2456789', 'orders@localbakery.mu'),
('S004', 'Snack and Grocery Supplies Ltd', '2567890', 'contact@snackgrocery.mu');

INSERT INTO Products (ProductId, Barcode, Title, Brand, CategoryId, SupplierId, Price, RestockDate)
VALUES
('P001', '6091000000011', 'Phoenix Drink', 'Phoenix', 'C001', 'S001', 80.00, '2026-07-05'),
('P002', '6091000000028', 'Apollo Instant Noodles', 'Apollo', 'C002', 'S002', 10.00, '2026-07-03'),
('P003', '6091000000035', 'Bread', 'Bakery', 'C003', 'S003', 7.00, '2026-06-30'),
('P004', '6091000000042', 'Doritos Nacho Cheese Flavoured Corn Chips', 'Frito-Lay', 'C004', 'S004', 45.00, '2026-07-08'),
('P005', '6091000000059', 'Brown Sugar', 'Dina', 'C004', 'S004', 90.00, '2026-07-06');

INSERT INTO Stock (ProductId, QuantityInStock, LowStockThreshold)
VALUES
('P001', 24, 5),
('P002', 12, 6),
('P003', 8, 4),
('P004', 10, 3),
('P005', 15, 5);

INSERT INTO Sales (SaleId, SaleDate, TotalAmount)
VALUES
('SALE001', '2026-06-28 10:30:00', 80.00),
('SALE002', '2026-06-28 11:15:00', 20.00);

INSERT INTO SaleItems (SaleId, ProductId, ProductTitle, QuantitySold, UnitPrice, ItemTotal)
VALUES
('SALE001', 'P001', 'Phoenix Drink', 1, 80.00, 80.00),
('SALE002', 'P002', 'Apollo Instant Noodles', 2, 10.00, 20.00);
GO