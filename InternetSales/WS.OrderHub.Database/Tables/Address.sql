CREATE TABLE [dbo].[Address]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [MiddleName] NVARCHAR(100) NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [Company] NVARCHAR(60) NULL, 
    [Street1] NVARCHAR(120) NOT NULL, 
    [Street2] NVARCHAR(120) NULL, 
    [Street3] NVARCHAR(120) NULL, 
    [City] NVARCHAR(120) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [PostalCode] NVARCHAR(20) NOT NULL, 
    [CountryCode] VARCHAR(5) NOT NULL, 
    [Phone] NVARCHAR(25) NULL, 
    [Fax] NVARCHAR(25) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NULL
)
