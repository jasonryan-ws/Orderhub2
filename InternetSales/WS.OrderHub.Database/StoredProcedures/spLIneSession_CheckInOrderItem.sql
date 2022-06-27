CREATE PROCEDURE [dbo].[spLineSession_CheckInOrderItem]
	@OrderId UNIQUEIDENTIFIER,
	@ProductId UNIQUEIDENTIFIER,
	@ReceivingQty INT,
	@CreatedByNodeId UNIQUEIDENTIFIER,
	@Feedback VARCHAR(100) OUTPUT
AS
BEGIN TRY
	IF @ReceivingQty = 0
		THROW 50001, 'Quantity submitted cannot be zero', 1;

	DECLARE @OrderItemId UNIQUEIDENTIFIER;
	DECLARE @ExpectedQty INT;

	-- Get the OrderItemId
	SELECT @OrderItemId = Id, @ExpectedQty = Quantity FROM OrderItem WHERE ProductId = @ProductId AND OrderId = @OrderId

	IF @OrderItemId IS NOT NULL
	BEGIN
		-- Get the current received qty
		DECLARE @ReceivedQty INT = (SELECT ISNULL(SUM(Quantity), 0) FROM LineSession WHERE ObjectId = @OrderItemId);
		
		-- Check if the receiving quantity could make the received qty negative
		IF (@ReceivedQty + @ReceivingQty) < 0
			THROW 50001, 'Total checked-in quantity cannot be negative', 1;

		IF (@ReceivedQty + @ReceivingQty) <= @ExpectedQty
		BEGIN
			BEGIN TRAN CheckInOrderItem

			INSERT INTO LineSession
				(Id, ObjectId, Quantity, DateCreated, CreatedByNodeId)
			VALUES
				(NEWID(), @OrderItemId, @ReceivingQty, GETDATE(), @CreatedByNodeId);

			COMMIT TRAN CheckInOrderItem;
			-- Get the new received qty after insertion
			DECLARE @NewReceivedQty INT = (SELECT ISNULL(SUM(Quantity), 0) FROM LineSession WHERE ObjectId = @OrderItemId);
			
			DECLARE @Result INT;
			IF @NewReceivedQty = @ExpectedQty
			BEGIN
				SET @Feedback = 'Item is fully checked-in';
				SET @Result = 200;
			END

			IF @NewReceivedQty > @ReceivedQty
			BEGIN
				SET @Feedback = 'Item quantity checked-in'
				SET @Result = 204;
			END
			ELSE
			BEGIN
				SET @Result = @ReceivingQty * -1;
				IF @Result > 1
					SET @Feedback = 'Item quantities have been removed';
				ELSE
					SET @Feedback = 'Item quantity has been removed';
			END
		END
		ELSE
		BEGIN
			SET @Feedback = 'Quantity exceeds the limit';
			SET @Result = 0;
		END

		RETURN @Result;
	END
	ELSE
		THROW 50001, 'Item cannot be found', 1;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH