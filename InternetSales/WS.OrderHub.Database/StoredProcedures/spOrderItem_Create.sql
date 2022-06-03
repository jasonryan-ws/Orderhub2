CREATE PROCEDURE [dbo].[spOrderItem_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
	@OrderId UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
	@Quantity INT,
	@UnitPrice MONEY,
	@ForceUpdate BIT = NULL
AS
BEGIN TRY
	SET @Id = (SELECT Id FROM OrderItem WHERE OrderId = @OrderId AND ProductId = @ProductId);
	IF @Id IS NULL
	BEGIN
		BEGIN TRAN CreateOrderItem
		SET @Id = NEWID();

		INSERT INTO OrderItem
			(Id, OrderId, ProductId, Quantity, UnitPrice)
		VALUES
			(@Id, @OrderId, @ProductId, @Quantity, @UnitPrice);
		COMMIT TRAN CreateOrderItem;
		RETURN @@ROWCOUNT;
	END
	ELSE IF @ForceUpdate = 1
		EXEC spOrderItem_Update @Id, @Quantity, @UnitPrice;
	ELSE IF @ForceUpdate IS NULL
		THROW 50001, 'Item already exists', 1;

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH
