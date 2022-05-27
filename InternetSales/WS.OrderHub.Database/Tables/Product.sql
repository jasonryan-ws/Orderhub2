CREATE TABLE [dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [SKU] VARCHAR(25) NOT NULL UNIQUE, 
    [UPC] VARCHAR(25) NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [ImageURL] NVARCHAR(MAX) NULL, 
    [DateCreated] DATETIME NOT NULL,
    [CreatedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [DateModified] DATETIME NULL,
    [ModifiedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DateDeleted] DATETIME NULL,
    [DeletedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id)
)
-- Creating Full-Text Catalog and Index for Product Description search
GO
CREATE FULLTEXT CATALOG [ftProduct] AS DEFAULT
GO
CREATE UNIQUE INDEX ui_product_id ON dbo.Product(Id); 
GO
CREATE FULLTEXT INDEX
	ON [dbo].[Product]
		(Name)
	KEY INDEX ui_product_id
    WITH STOPLIST = OFF;