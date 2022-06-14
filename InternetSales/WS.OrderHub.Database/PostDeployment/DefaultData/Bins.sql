INSERT INTO Bin
	(Id, Name, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'TestBin-B', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), 'TestBin-M', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-SERVER')),
	(NEWID(), 'TestBin-T', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01'))