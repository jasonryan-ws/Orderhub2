
-- Create a new season for verifying order items or vendor puchase order items
-- Object Id could be an OrderItemid or PurchaseOrderItemId
CREATE TABLE [dbo].[LineSession]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[ObjectId] UNIQUEIDENTIFIER NOT NULL,
	[Quantity] INT NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[CreatedbyNodeId] UNIQUEIDENTIFIER NOT NULL
)
