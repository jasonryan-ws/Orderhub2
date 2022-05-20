
-- Database connection of the data provider (ShipWorks)
CREATE TABLE [dbo].[Connection]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(25) NOT NULL UNIQUE,
    [Server] VARCHAR(100) NOT NULL, 
    [Database] VARCHAR(50) NOT NULL, 
    [IsIntegrated] BIT NOT NULL, 
    [UserId] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(MAX) NOT NULL,
    [DateModified] DATETIME NULL
)
