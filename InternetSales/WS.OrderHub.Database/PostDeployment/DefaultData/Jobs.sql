INSERT INTO Job
	(Id, TaskId, DateStarted, StartedByNodeId, MaxCount)
VALUES
	(NEWID(), (SELECT Id FROM Task WHERE Name = 'Update Orders'), GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01'), 2118)