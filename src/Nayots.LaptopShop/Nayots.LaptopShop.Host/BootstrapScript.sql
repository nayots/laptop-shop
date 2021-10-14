Create Table Products (
  ID INTEGER PRIMARY KEY ,
  Name NVARCHAR(256) NOT NULL,
  Price REAL  NOT NULL,
  ProductType INT NOT NULL
);

INSERT INTO Products ([Name], Price, ProductType)
VALUES
    ('Dell', 349.87, 1),
    ('Toshiba', 345.67,1),
    ('HP', 456.76,1),
    ('Lenovo', 1000,1),
    ('MacBook Pro', 1500,1),
    ('Corsair 8GB', 45.67,2),
    ('Corsair 16GB(2x8GB)', 87.88,2),
    ('Seagate 500GB HDD', 123.34,3),
    ('Seagate 1TB HDD', 200.00,3),
    ('Samsung 500GB SSD', 130.20,3),
    ('Samsung 1TB SSD', 180,3),
    ('Blue', 34.56,4),
    ('Red', 50.76,4),
    ('Space Gray', 80,4),
    ('Rose Gold', 80,4);

Create Table CartItems (
  UserID INTEGER NOT NULL,
  ProductID INTEGER NOT NULL,
  FOREIGN KEY (ProductID) REFERENCES Products(ID) ON DELETE CASCADE,
  PRIMARY KEY(UserID, ProductID)
);