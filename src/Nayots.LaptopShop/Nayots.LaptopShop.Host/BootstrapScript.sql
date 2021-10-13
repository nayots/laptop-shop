Create Table Products (
  ID INT IDENTITY(1, 1) PRIMARY KEY,
  Name NVARCHAR(256) NOT NULL,
  Price DECIMAL(19, 4) NOT NULL,
  ProductType INT NOT NULL
);