CREATE PROCEDURE [dbo].[spOrderCharge_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
	@OrderId UNIQUEIDENTIFIER,
	@ChargeId UNIQUEIDENTIFIER,
	@Amount MONEY,
	@ForceUpdate BIT = 0
AS
BEGIN TRY
	SET @Id = (SELECT Id FROM OrderCharge WHERE OrderId = @OrderId AND ChargeId = @ChargeId)
	IF @Id IS NULL
	BEGIN
		BEGIN TRAN CreateOrderCharge;
		SET @Id = NEWID();
		INSERT INTO OrderCharge
			(Id, OrderId, ChargeId, Amount)
		VALUES
			(@Id, @OrderId, @ChargeId, @Amount);
		COMMIT TRAN CreateOrderCharge;
		RETURN @@ROWCOUNT;
	END
	ELSE IF @ForceUpdate = 1
		EXEC spOrderCharge_Update @Id, @Amount
	ELSE
		THROW 50001, 'Order charge already exists', 1;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 1
		ROLLBACK TRAN;
	THROW;
END CATCH