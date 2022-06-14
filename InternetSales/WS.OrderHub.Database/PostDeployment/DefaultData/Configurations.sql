
DECLARE @ModifiedByNodeId UNIQUEIDENTIFIER = (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01');
DECLARE @StoreAddressId VARCHAR(MAX) = (SELECT Id FROM Address WHERE Email = 'support@wheelandsprocket.com')
DECLARE @ReceiptFooter VARCHAR(MAX) =
'We''d like to hear from you: review our product; review us; or join us online at facebook.com/wheelandsprocket' + CHAR(13) +
'How was the packaging and delivery of today''s order? Let us know at: wheelandsprocket.com/goto/delivery' + CHAR(13) +
'For returns or exchanges; please contact us to receive a Return Merchandise Authorization (RMA) number' + CHAR(13) +
'Email: returns@wheelandsprocket.com' + CHAR(13) +
'USA Toll-Free: +1 866-995-9918';

INSERT INTO [Configuration]
	(Id, Name, Value, Description, FullDescription, DateModified, ModifiedByNodeId)
VALUES
	(NEWID(), 'StoreAddressId', @StoreAddressId, 'Store Address ID', 'Store Address ID', GETDATE(), @ModifiedByNodeId),
	(NEWID(), 'ReceiptFooter', @ReceiptFooter, 'Receipt Footer', 'Seller''s message to the customer that will appear on the bottom of the order receipt', GETDATE(), @ModifiedByNodeId),
	(NEWID(), 'SWServer', 'IS-SERVER', 'Server name or IP address', 'ShipWorks server NetBIOS name or IP address', GETDATE(), @ModifiedByNodeId),
	(NEWID(), 'SWUserId', 'SA', 'Server username', 'ShipWorks server username', GETDATE(),  @ModifiedByNodeId), 
	(NEWID(), 'SWPassword', 'ZDiTJK7V0dY=', 'Server password', 'ShipWorks server password', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SWDatabase', 'ShipWorks', 'Database name', 'ShipWorks server database name', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SWIntegrated', 'False', 'Windows authentication', 'ShipWorks server authentication', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SWStoreId', '23005', 'Store ID', 'ShipWorks store ID', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SVEmail', 'jason.ryan@wheelandsprocket.com', 'Login email', 'SkuVault login email', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SVPassword', 'VGhIOJeNoDU4k/Bk1fnGJw==', 'Login password', 'SkuVault login password', GETDATE(), @ModifiedByNodeId), 
	(NEWID(), 'SVWarehouseId', '1402', 'Warehouse ID', 'SkuVault warehouse ID', GETDATE(), @ModifiedByNodeId)
