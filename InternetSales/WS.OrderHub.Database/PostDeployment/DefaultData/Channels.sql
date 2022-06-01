INSERT INTO Channel
	(Id, Name, Code, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'Amazon', 'AZ', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-JASON')),
	(NEWID(), 'eBay', 'EB', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-JASON')),
	(NEWID(), 'Walmart', 'WM', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER'))