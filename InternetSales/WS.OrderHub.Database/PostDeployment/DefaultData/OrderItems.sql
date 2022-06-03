INSERT INTO OrderItem
	(Id, OrderId, ProductId, Quantity, UnitPrice)
VALUES
	(NEWID(), (SELECT Id FROM [Order] WHERE ChannelOrderNumber = '1345756'), (SELECT Id FROM Product WHERE SKU = '643187000557'), 1, 6.99),
	(NEWID(), (SELECT Id FROM [Order] WHERE ChannelOrderNumber = '1345756'), (SELECT Id FROM Product WHERE SKU = '689228561601'), 2, 12.99),
	(NEWID(), (SELECT Id FROM [Order] WHERE ChannelOrderNumber = '1345756'), (SELECT Id FROM Product WHERE SKU = '4710944224184'), 3, 24.99)