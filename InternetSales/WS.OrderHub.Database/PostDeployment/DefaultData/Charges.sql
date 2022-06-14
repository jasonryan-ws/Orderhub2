INSERT INTO Charge
	(Id, Name, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'Tax', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER')),
	(NEWID(), 'Discount', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), 'Shipping', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER'))