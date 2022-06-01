INSERT INTO Channel
	(Id, Name, Code, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'Amazon', 'AMZ', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-JASON')),
	(NEWID(), 'eBay', 'EBY', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-JASON')),
	(NEWID(), 'Walmart', 'WMT', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER'))