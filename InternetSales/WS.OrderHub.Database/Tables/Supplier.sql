CREATE TABLE [dbo].[Supplier]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL UNIQUE, 
    [Code] VARCHAR(15) NOT NULL UNIQUE,
    [ColorCode] INT NOT NULL,
    [DateCreated] DATETIME NOT NULL,
    [CreatedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [DateModified] DATETIME NULL,
    [ModifiedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DateDeleted] DATETIME NULL,
    [DeletedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id)
)

-- Creating Full-Text Catalog and Index for Supplier name search
GO
CREATE FULLTEXT CATALOG [ftSupplier] AS DEFAULT
GO
CREATE UNIQUE INDEX ui_supplier_id ON dbo.Supplier(Id); 
GO
CREATE FULLTEXT INDEX
	ON [dbo].[Supplier]
		(Name)
	KEY INDEX ui_supplier_id
    WITH STOPLIST = OFF;
