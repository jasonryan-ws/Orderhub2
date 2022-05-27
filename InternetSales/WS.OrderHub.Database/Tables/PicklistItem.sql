CREATE TABLE [dbo].[PicklistItem]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PickListId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Picklist](Id) ON DELETE CASCADE, 
    [OrderId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[Order](Id),
    [ProductId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[Product](Id), 
    [Location] VARCHAR(255) NULL, 
    [LocationDateUpdated] DATETIME NULL,
    UNIQUE(PicklistId, OrderId, ProductId)
)
