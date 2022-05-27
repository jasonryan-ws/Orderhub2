CREATE TABLE [dbo].[SupplierItem]
(
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	[SupplierId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[Supplier]([Id]) ON DELETE CASCADE, 
    [ProductId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[Product]([Id]) ON DELETE CASCADE,
	[PartNumber] NVARCHAR(25) NULL,
	[IsPrimary] BIT NOT NULL, --Supplier part number will appear on the product label if allowed
    [CreatedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [DateModified] DATETIME NULL,
    [ModifiedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id)
	UNIQUE(SupplierId, ProductId)
)
