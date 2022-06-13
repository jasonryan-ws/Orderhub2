INSERT INTO Job
	(Id, TaskId, DateStarted, StartedByNodeId)
VALUES
	(NEWID(), (SELECT Id FROM Task WHERE Name = 'Update Orders'), GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-JASON'))