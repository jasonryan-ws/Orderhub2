-- Delete addresses that are not being used in any Orders or not a current store address

CREATE PROCEDURE [dbo].[spAddress_DeleteUnused]
AS
BEGIN TRY
	BEGIN TRAN DeleteUnusedAddresses
	DELETE FROM [Address]
	WHERE
		Id NOT IN (SELECT BillAddressId FROM [Order]) AND
		Id NOT IN (SELECT ShipAddressId FROM [Order]) AND
		Id NOT IN (SELECT [Value] FROM Configuration WHERE Name = 'StoreAddressId');
	COMMIT TRAN;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH