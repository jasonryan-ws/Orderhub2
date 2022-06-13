DECLARE @CreatorId UNIQUEIDENTIFIER = (SELECT Id FROM Node WHERE Name = 'IS-JASON');
DECLARE @ChannelId UNIQUEIDENTIFIER = (SELECT Id FROM Channel WHERE Name = 'Amazon');
DECLARE @AddressId UNIQUEIDENTIFIER = (SELECT Id FROM Address WHERE FirstName = 'JOHN' AND LastName = 'Doe');
INSERT INTO [Order]
	(Id, DateOrdered, ChannelId, ChannelOrderNumber, BillAddressId, ShipAddressId, DateCreated, CreatedByNodeId, ExternalRowVersion)
VALUES
	(NEWID(), '2022-05-13' , @ChannelId, '1345756', @AddressId, @AddressId, GETDATE(), @CreatorId, 0x000000000DC95FC1)