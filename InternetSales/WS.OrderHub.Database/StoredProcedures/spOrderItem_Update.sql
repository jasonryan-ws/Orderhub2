CREATE PROCEDURE [dbo].[spOrderItem_Update]
	@Id UNIQUEIDENTIFIER,
	@Quantity INT,
	@UnitPrice MONEY
AS
BEGIN TRY
	BEGIN TRAN UpdateOrderItem
	UPDATE OrderItem
	SET
		Quantity = @Quantity,
		UnitPrice = @UnitPrice
	WHERE
		Id = @Id;
	COMMIT TRAN UpdateOrderItem;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH