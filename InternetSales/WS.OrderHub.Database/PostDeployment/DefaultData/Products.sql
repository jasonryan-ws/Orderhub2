INSERT INTO Product
	(Id, SKU, UPC, Name, ImageURL, DateCreated, CreatedByNodeId)
VALUES
	(NEWID(), '689228561601', '689228561601', 'Shimano R160D/D Disc Brake Adaptor Flat Mount 160mm', 'https://assets.ws/images/products/qbp/q/BR8732.jpg', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), '4710944224184', '4710944224184', 'Sturmey Archer Mark II Indicator Chain 3 speed', 'https://images.qbp.com/imageservice/image/c1d9f140135e/prodxl/HU2226.jpg', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), '643187000557', '643187000557', 'ZEFAL Bicycle Toe Clips For Use on Any Flat Pedal - L/XL - // No Strap // Black', 'http://az416554.vo.msecnd.net/86d0cb1ad9044e41991c0aeb852dc364/Images/Products49383-600x617-224549.jpg', GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), '689228389267', '689228389267', 'Shimano AD17-M Front Derailleur Clamp Shim, reduces 34.9mm to 31.8mm', null, GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01')),
	(NEWID(), '047853625769', '047853625769', 'Kenda Tire K40 24X1-3/8" Black (37X540)', null, GETDATE(), (SELECT Id FROM Node WHERE Name = 'IS-AUTH-01'))