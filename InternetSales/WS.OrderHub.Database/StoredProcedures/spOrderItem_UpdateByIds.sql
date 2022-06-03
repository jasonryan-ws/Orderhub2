CREATE PROCEDURE [dbo].[spOrderItem_UpdateByIds]
	@OrderId UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
	@Quantity INT,
	@UnitPrice MONEY
AS
	DECLARE @Id UNIQUEIDENTIFIER = (SELECT Id FROM OrderItem WHERE OrderId = @OrderId AND ProductId = @ProductId)
	IF @Id IS NOT NULL
		EXEC spOrderItem_Update @Id, @Quantity, @UnitPrice;
RETURN 0
