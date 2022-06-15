CREATE PROCEDURE [dbo].[spOrder_Update]
	@Id UNIQUEIDENTIFIER,
    @Status NVARCHAR(50), 
    @ShipMethod NVARCHAR(100), 
    @IsShipped BIT, 
    @DateShipped DATETIME,
    @ShipCost MONEY,
    @IsCancelled BIT,
    @DateCancelled DATETIME,
    @CancelledByNodeId UNIQUEIDENTIFIER,
    @Comments NVARCHAR(MAX),  
    @ModifiedByNodeId UNIQUEIDENTIFIER,
    @ExternalRowVersion BINARY(8)
AS
BEGIN TRY
    BEGIN TRAN UpdateOrder
    UPDATE [Order]
    SET
        [Status] = @Status,
        ShipMethod = @ShipMethod,
        IsShipped = @IsShipped,
        DateShipped = @DateShipped,
        ShipCost = @ShipCost,
        IsCancelled = @IsCancelled,
        DateCancelled = @DateCancelled,
        CancelledByNodeId = @CancelledByNodeId,
        Comments = @Comments,
        DateModified = GETDATE(),
        ModifiedByNodeId = @ModifiedByNodeId,
        ExternalRowVersion = @ExternalRowVersion
    WHERE
        Id = @Id;
    COMMIT TRAN UpdateOrder;
    RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH