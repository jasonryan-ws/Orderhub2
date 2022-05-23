INSERT INTO [Address]
	(Id, FirstName, LastName, Company, Street1, City, State, PostalCode, CountryCode, Phone, Email, DateCreated)
VALUES
	(NEWID(), 'Daniel', 'Solie', 'Wheel & Sprocket', '3939 W College Ave', 'Appleton', 'WI', '54914', 'US', '866-995-9918', 'support@wheelandsprocket.com', GETDATE())