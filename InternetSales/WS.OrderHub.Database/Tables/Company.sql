
-- Store Location
-- Active Store address will be used for the customer receipt
CREATE TABLE [dbo].[Company]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL UNIQUE, 
    [Code] NVARCHAR(5) NOT NULL UNIQUE,
    [AddressId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES dbo.Address(Id), 
    [ConnectionId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES dbo.Connection(Id), 
    [IsActive] BIT NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NULL
)
