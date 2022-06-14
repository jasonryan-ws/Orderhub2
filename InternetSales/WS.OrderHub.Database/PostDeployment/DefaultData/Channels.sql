INSERT INTO Channel
	(Id, Name, Code, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'Amazon', 'AMZ', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), 'eBay', 'EBY', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), 'Walmart', 'WMT', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER'))