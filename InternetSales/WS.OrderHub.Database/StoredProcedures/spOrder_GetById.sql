CREATE PROCEDURE [dbo].[spOrder_GetById]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT
		o.*,

		-- Channel
		c.Name as 'ChannelName',
		c.Code as 'ChannelCode',
		c.ColorCode as 'ChannelColorCode',

		-- Billing Address
		bill.FirstName as 'BillFirstName',
		bill.MiddleName as 'BillMiddleName',
		bill.LastName as 'BillLastName',
		bill.Company as 'BillCompany',
		bill.Street1 as 'BillStreet1',
		bill.Street2 as 'BillStreet2',
		bill.Street3 as 'BillStreet3',
		bill.City as 'BillCity',
		bill.State as 'BillStateCode',
		ISNULL(billState.Name, bill.State) as 'BillStateName',
		bill.PostalCode as 'BillPostalCode',
		bill.CountryCode as 'BillCountryCode',
		ISNULL(billCountry.Name, bill.CountryCode) as 'BillCountryName',
		bill.Phone as 'BillPhone',
		bill.Fax as 'BillFax',
		bill.Email as 'BillEmail',


		-- Shipping Address
		ship.FirstName as 'ShipFirstName',
		ship.MiddleName as 'ShipMiddleName',
		ship.LastName as 'ShipLastName',
		ship.Company as 'ShipCompany',
		ship.Street1 as 'ShipStreet1',
		ship.Street2 as 'ShipStreet2',
		ship.Street3 as 'ShipStreet3',
		ship.City as 'ShipCity',
		ship.State as 'ShipStateCode',
		ISNULL(shipState.Name, ship.State) as 'ShipStateName',
		ship.PostalCode as 'ShipPostalCode',
		ship.CountryCode as 'ShipCountryCode',
		ISNULL(shipCountry.Name, ship.CountryCode) as 'ShipCountryName',
		ship.Phone as 'ShipPhone',
		ship.Fax as 'ShipFax',
		ship.Email as 'ShipEmail'
	FROM [Order] o
	LEFT JOIN Channel c ON c.Id = o.ChannelId
	LEFT JOIN Address bill ON bill.Id = o.BillAddressId
	LEFT JOIN Address ship ON ship.Id = o.ShipAddressId
	LEFT JOIN State billState ON billState.Code = bill.State AND billState.CountryId = (SELECT Id FROM Country WHERE Code = bill.CountryCode)
	LEFT JOIN State shipState ON shipState.Code = ship.State AND shipState.CountryId = (SELECT Id FROM Country WHERE Code = ship.CountryCode)
	LEFT JOIN Country billCountry ON billCountry.Code = bill.CountryCode
	LEFT JOIN Country shipCountry ON shipCountry.Code = ship.CountryCode
	WHERE
		o.Id = @Id
END