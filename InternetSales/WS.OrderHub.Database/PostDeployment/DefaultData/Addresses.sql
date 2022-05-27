DECLARE @CreatedByNodeId UNIQUEIDENTIFIER = (SELECT Id FROM Node WHERE Name = 'IS-JASON');

INSERT INTO [Address]
	(Id, FirstName, LastName, Company, Street1, Street2, City, State, PostalCode, CountryCode, Phone, Email, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), 'Daniel', 'Solie', 'Wheel & Sprocket', '3939 W College Ave', '' , 'Appleton', 'WI', '54914', 'US', '866-995-9918', 'support@wheelandsprocket.com', GETDATE(), @CreatedByNodeId),
	(NEWID(), 'Jason', 'Ryan', 'Wheel & Sprocket', '3939 W College Ave', 'Internet Sales', 'Appleton', 'WI', '54914', 'US', '866-995-9918', 'jason.ryan@wheelandsprocket.com', GETDATE(), @CreatedByNodeId),
	(NEWID(), 'DJ', 'Brooks', 'Wheel & Sprocket', '3939 W College Ave', 'Internet Sales', 'Appleton', 'WI', '54914', 'US', '866-995-9918', 'dj.brooks@wheelandsprocket.com', GETDATE(), @CreatedByNodeId)